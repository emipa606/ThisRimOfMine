using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace TROM;

internal class GenStep_TROM_Terrain : GenStep
{
    private static bool debug_WarnedMissingTerrain;

    private static readonly HashSet<IntVec3> tmpVisited = new HashSet<IntVec3>();

    private static readonly List<IntVec3> tmpIsland = new List<IntVec3>();

    public override int SeedPart => 262606459;

    public override void Generate(Map map, GenStepParams parms)
    {
        BeachMaker.Init(map);
        GenerateRiver(map);
        var elevation = MapGenerator.Elevation;
        var fertility = MapGenerator.Fertility;
        var terrainGrid = map.terrainGrid;
        foreach (var allCell in map.AllCells)
        {
            var edifice = allCell.GetEdifice(map);
            var terrainDef = TerrainFrom(allCell, map, elevation[allCell], fertility[allCell]);
            if (terrainDef.IsRiver && edifice != null)
            {
                edifice.Destroy();
            }

            terrainGrid.SetTerrain(allCell, terrainDef);
        }

        foreach (var terrainPatchMaker in map.Biome.terrainPatchMakers)
        {
            terrainPatchMaker.Cleanup();
        }
    }

    private TerrainDef TerrainFrom(IntVec3 c, Map map, float elevation, float fertility)
    {
        TerrainDef terrainDef2;

        foreach (var terrainPatchMaker in map.Biome.terrainPatchMakers)
        {
            terrainDef2 = terrainPatchMaker.TerrainAt(c, map, fertility);
            if (terrainDef2 != null)
            {
                return terrainDef2;
            }
        }

        if (elevation > 0.7f)
        {
            return TerrainDefOf.Gravel;
        }

        terrainDef2 = TerrainThreshold.TerrainAtValue(map.Biome.terrainsByFertility, fertility);
        if (terrainDef2 != null)
        {
            return terrainDef2;
        }

        if (debug_WarnedMissingTerrain)
        {
            return TerrainDefOf.Sand;
        }

        Log.Error($"No terrain found in biome {map.Biome.defName} for elevation={elevation}, fertility={fertility}");
        debug_WarnedMissingTerrain = true;

        return TerrainDefOf.Sand;
    }

    private void RemoveIslands(Map map)
    {
        var mapRect = CellRect.WholeMap(map);
        var num = 0;
        tmpVisited.Clear();
        foreach (var allCell in map.AllCells)
        {
            if (tmpVisited.Contains(allCell) || Impassable(allCell))
            {
                continue;
            }

            var area = 0;
            var touchesMapEdge2 = false;
            map.floodFiller.FloodFill(allCell, x => !Impassable(x), delegate(IntVec3 x)
            {
                tmpVisited.Add(x);
                area++;
                if (mapRect.IsOnEdge(x))
                {
                    touchesMapEdge2 = true;
                }
            });
            if (touchesMapEdge2)
            {
                num = Mathf.Max(num, area);
            }
        }

        if (num < 30)
        {
            return;
        }

        tmpVisited.Clear();
        foreach (var allCell2 in map.AllCells)
        {
            if (tmpVisited.Contains(allCell2) || Impassable(allCell2))
            {
                continue;
            }

            tmpIsland.Clear();
            TerrainDef adjacentImpassableTerrain = null;
            var touchesMapEdge = false;
            map.floodFiller.FloodFill(allCell2, delegate(IntVec3 x)
            {
                if (!Impassable(x))
                {
                    return true;
                }

                adjacentImpassableTerrain = x.GetTerrain(map);
                return false;
            }, delegate(IntVec3 x)
            {
                tmpVisited.Add(x);
                tmpIsland.Add(x);
                if (mapRect.IsOnEdge(x))
                {
                    touchesMapEdge = true;
                }
            });
            if (tmpIsland.Count > num / 20 && (touchesMapEdge || tmpIsland.Count >= num / 2) ||
                adjacentImpassableTerrain == null)
            {
                continue;
            }

            for (var i = 0; i < tmpIsland.Count; i++)
            {
                map.terrainGrid.SetTerrain(tmpIsland[i], adjacentImpassableTerrain);
            }
        }

        bool Impassable(IntVec3 x)
        {
            return x.GetTerrain(map).passability == Traversability.Impassable;
        }
    }

    private void GenerateRiver(Map map)
    {
        var rivers = Find.WorldGrid[map.Tile].Rivers;
        if (rivers == null || rivers.Count == 0)
        {
            return;
        }

        var angle = Find.WorldGrid.GetHeadingFromTo(map.Tile,
            rivers.OrderBy(rl => -rl.river.degradeThreshold).First().neighbor);
        var rot = Find.World.CoastDirectionAt(map.Tile);
        if (rot != Rot4.Invalid)
        {
            angle = rot.AsAngle + Rand.RangeInclusive(-30, 30);
        }

        var riverMaker =
            new RiverMaker(new Vector3(Rand.Range(0.3f, 0.7f) * map.Size.x, 0f, Rand.Range(0.3f, 0.7f) * map.Size.z),
                angle, rivers.OrderBy(rl => -rl.river.degradeThreshold).FirstOrDefault().river);
        GenerateRiverLookupTexture(map, riverMaker);
    }

    private void UpdateRiverAnchorEntry(Dictionary<int, GRLT_Entry> entries, IntVec3 center, int entryId, float zValue)
    {
        var num = zValue - entryId;
        if (!(num > 2f) && (!entries.ContainsKey(entryId) || entries[entryId].bestDistance > num))
        {
            entries[entryId] = new GRLT_Entry
            {
                bestDistance = num,
                bestNode = center
            };
        }
    }

    private void GenerateRiverLookupTexture(Map map, RiverMaker riverMaker)
    {
        var num = Mathf.CeilToInt(DefDatabase<RiverDef>.AllDefs.Select(rd => (rd.widthOnMap / 2f) + 8f).Max());
        var num2 = Mathf.Max(4, num) * 2;
        var dictionary = new Dictionary<int, GRLT_Entry>();
        var dictionary2 = new Dictionary<int, GRLT_Entry>();
        var dictionary3 = new Dictionary<int, GRLT_Entry>();
        for (var i = -num2; i < map.Size.z + num2; i++)
        {
            for (var j = -num2; j < map.Size.x + num2; j++)
            {
                var intVec = new IntVec3(j, 0, i);
                var vector = riverMaker.WaterCoordinateAt(intVec);
                var entryId = Mathf.FloorToInt(vector.z / 4f);
                UpdateRiverAnchorEntry(dictionary, intVec, entryId, (vector.z + Mathf.Abs(vector.x)) / 4f);
                UpdateRiverAnchorEntry(dictionary2, intVec, entryId, (vector.z + Mathf.Abs(vector.x - num)) / 4f);
                UpdateRiverAnchorEntry(dictionary3, intVec, entryId, (vector.z + Mathf.Abs(vector.x + num)) / 4f);
            }
        }

        var num3 = Mathf.Max(dictionary.Keys.Min(), dictionary2.Keys.Min(), dictionary3.Keys.Min());
        var num4 = Mathf.Min(dictionary.Keys.Max(), dictionary2.Keys.Max(), dictionary3.Keys.Max());
        for (var k = num3; k < num4; k++)
        {
            var waterInfo = map.waterInfo;
            if (dictionary2.ContainsKey(k) && dictionary2.ContainsKey(k + 1))
            {
                waterInfo.riverDebugData.Add(dictionary2[k].bestNode.ToVector3Shifted());
                waterInfo.riverDebugData.Add(dictionary2[k + 1].bestNode.ToVector3Shifted());
            }

            if (dictionary.ContainsKey(k) && dictionary.ContainsKey(k + 1))
            {
                waterInfo.riverDebugData.Add(dictionary[k].bestNode.ToVector3Shifted());
                waterInfo.riverDebugData.Add(dictionary[k + 1].bestNode.ToVector3Shifted());
            }

            if (dictionary3.ContainsKey(k) && dictionary3.ContainsKey(k + 1))
            {
                waterInfo.riverDebugData.Add(dictionary3[k].bestNode.ToVector3Shifted());
                waterInfo.riverDebugData.Add(dictionary3[k + 1].bestNode.ToVector3Shifted());
            }

            if (dictionary2.ContainsKey(k) && dictionary.ContainsKey(k))
            {
                waterInfo.riverDebugData.Add(dictionary2[k].bestNode.ToVector3Shifted());
                waterInfo.riverDebugData.Add(dictionary[k].bestNode.ToVector3Shifted());
            }

            if (!dictionary.ContainsKey(k) || !dictionary3.ContainsKey(k))
            {
                continue;
            }

            waterInfo.riverDebugData.Add(dictionary[k].bestNode.ToVector3Shifted());
            waterInfo.riverDebugData.Add(dictionary3[k].bestNode.ToVector3Shifted());
        }

        var cellRect = new CellRect(-2, -2, map.Size.x + 4, map.Size.z + 4);
        var array = new float[cellRect.Area * 2];
        var num5 = 0;
        for (var l = cellRect.minZ; l <= cellRect.maxZ; l++)
        {
            for (var m = cellRect.minX; m <= cellRect.maxX; m++)
            {
                var intVec2 = new IntVec3(m, 0, l);
                var river = true;
                foreach (var intVec3 in GenAdj.AdjacentCellsAndInside)
                {
                    if (riverMaker.TerrainAt(intVec2 + intVec3) == null)
                    {
                        continue;
                    }

                    river = false;
                    break;
                }

                if (!river)
                {
                    var p = intVec2.ToIntVec2.ToVector2();
                    var num6 = int.MinValue;
                    var vector2 = Vector2.zero;
                    for (var num7 = num3; num7 < num4; num7++)
                    {
                        if (!dictionary2.ContainsKey(num7) || !dictionary2.ContainsKey(num7 + 1) ||
                            !dictionary.ContainsKey(num7) || !dictionary.ContainsKey(num7 + 1) ||
                            !dictionary3.ContainsKey(num7) || !dictionary3.ContainsKey(num7 + 1))
                        {
                            continue;
                        }

                        var p2 = dictionary2[num7].bestNode.ToIntVec2.ToVector2();
                        var p3 = dictionary2[num7 + 1].bestNode.ToIntVec2.ToVector2();
                        var p4 = dictionary[num7].bestNode.ToIntVec2.ToVector2();
                        var p5 = dictionary[num7 + 1].bestNode.ToIntVec2.ToVector2();
                        var p6 = dictionary3[num7].bestNode.ToIntVec2.ToVector2();
                        var p7 = dictionary3[num7 + 1].bestNode.ToIntVec2.ToVector2();
                        var vector3 = GenGeo.InverseQuadBilinear(p, p4, p2, p5, p3);
                        if (vector3.x >= -0.0001f && vector3 is { x: <= 1.0001f, y: >= -0.0001f and <= 1.0001f })
                        {
                            vector2 = new Vector2((0f - vector3.x) * num, (vector3.y + num7) * 4f);
                            num6 = num7;
                            break;
                        }

                        var vector4 = GenGeo.InverseQuadBilinear(p, p4, p6, p5, p7);
                        if (!(vector4.x >= -0.0001f) || vector4 is not { x: <= 1.0001f, y: >= -0.0001f and <= 1.0001f })
                        {
                            continue;
                        }

                        vector2 = new Vector2(vector4.x * num, (vector4.y + num7) * 4f);
                        num6 = num7;
                        break;
                    }

                    if (num6 == int.MinValue)
                    {
                        Log.ErrorOnce("Failed to find all necessary river flow data", 5273133);
                    }

                    array[num5] = vector2.x;
                    array[num5 + 1] = vector2.y;
                }

                num5 += 2;
            }
        }

        var array2 = new float[cellRect.Area * 2];
        var array3 = new[]
            { 0.123317f, 0.123317f, 0.123317f, 0.123317f, 0.077847f, 0.077847f, 0.077847f, 0.077847f, 0.195346f };
        var num8 = 0;
        for (var num9 = cellRect.minZ; num9 <= cellRect.maxZ; num9++)
        {
            for (var num10 = cellRect.minX; num10 <= cellRect.maxX; num10++)
            {
                var intVec3 = new IntVec3(num10, 0, num9);
                var num11 = 0f;
                var num12 = 0f;
                var num13 = 0f;
                for (var num14 = 0; num14 < GenAdj.AdjacentCellsAndInside.Length; num14++)
                {
                    var c = intVec3 + GenAdj.AdjacentCellsAndInside[num14];
                    if (!cellRect.Contains(c))
                    {
                        continue;
                    }

                    var num15 = num8 + ((GenAdj.AdjacentCellsAndInside[num14].x +
                                         (GenAdj.AdjacentCellsAndInside[num14].z * cellRect.Width)) * 2);
                    if (array[num15] == 0f && array[num15 + 1] == 0f)
                    {
                        continue;
                    }

                    num11 += array[num15] * array3[num14];
                    num12 += array[num15 + 1] * array3[num14];
                    num13 += array3[num14];
                }

                if (num13 > 0f)
                {
                    array2[num8] = num11 / num13;
                    array2[num8 + 1] = num12 / num13;
                }

                num8 += 2;
            }
        }

        array = array2;
        for (var num16 = 0; num16 < array.Length; num16 += 2)
        {
            if (array[num16] == 0f && array[num16 + 1] == 0f)
            {
                continue;
            }

            var vector5 = Rand.InsideUnitCircle * 0.4f;
            array[num16] += vector5.x;
            array[num16 + 1] += vector5.y;
        }

        var array4 = new byte[array.Length * 4];
        Buffer.BlockCopy(array, 0, array4, 0, array.Length * 4);
        map.waterInfo.riverOffsetMap = array4;
        map.waterInfo.GenerateRiverFlowMap();
    }

    private struct GRLT_Entry
    {
        public float bestDistance;

        public IntVec3 bestNode;
    }
}