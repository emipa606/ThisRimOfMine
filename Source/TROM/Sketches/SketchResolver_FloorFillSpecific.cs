using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.SketchGen;
using UnityEngine;
using Verse;

namespace TROM.Sketches;

public class SketchResolver_FloorFillSpecific : SketchResolver
{
    private static readonly HashSet<IntVec3> tmpWalls = [];

    private static readonly HashSet<IntVec3> tmpVisited = [];

    private static readonly Stack<Pair<int, int>> tmpStack = new Stack<Pair<int, int>>();

    private static readonly List<IntVec3> tmpCells = [];

    private static ThingDef floorStuff;

    protected override void ResolveInt(ResolveParams parms)
    {
        floorStuff = parms.thingCentral;
        var terrainDef = TerrainDefOf.WoodPlankFloor;
        if (floorStuff == ThingDefOf.WoodLog)
        {
            terrainDef = TerrainDefOf.WoodPlankFloor;
        }
        else if (floorStuff == ThingDefOf.BlocksGranite)
        {
            terrainDef = TROMTerrainDefOf.TileGranite;
        }
        else if (floorStuff == TROMThingDefOf.BlocksLimestone)
        {
            terrainDef = TROMTerrainDefOf.TileLimestone;
        }
        else if (floorStuff == TROMThingDefOf.BlocksMarble)
        {
            terrainDef = TROMTerrainDefOf.TileMarble;
        }
        else if (floorStuff == TROMThingDefOf.BlocksSandstone)
        {
            terrainDef = TerrainDefOf.TileSandstone;
        }
        else if (floorStuff == TROMThingDefOf.BlocksSlate)
        {
            terrainDef = TROMTerrainDefOf.TileSlate;
        }
        else if (floorStuff == TROMThingDefOf.ChunkGranite)
        {
            terrainDef = TROMTerrainDefOf.FlagstoneGranite;
        }
        else if (floorStuff == TROMThingDefOf.ChunkLimestone)
        {
            terrainDef = TROMTerrainDefOf.FlagstoneLimestone;
        }
        else if (floorStuff == TROMThingDefOf.ChunkMarble)
        {
            terrainDef = TROMTerrainDefOf.FlagstoneMarble;
        }
        else if (floorStuff == TROMThingDefOf.ChunkSandstone)
        {
            terrainDef = TerrainDefOf.FlagstoneSandstone;
        }
        else if (floorStuff == TROMThingDefOf.ChunkSlate)
        {
            terrainDef = TROMTerrainDefOf.FlagstoneSlate;
        }

        var outerRect = parms.rect ?? parms.sketch.OccupiedRect;
        if (parms.floorFillRoomsOnly ?? false)
        {
            tmpWalls.Clear();
            foreach (var sketchThing in parms.sketch.Things)
            {
                if (sketchThing.def.passability != Traversability.Impassable ||
                    sketchThing.def.Fillage != FillCategory.Full)
                {
                    continue;
                }

                foreach (var item in sketchThing.OccupiedRect)
                {
                    tmpWalls.Add(item);
                }
            }

            tmpVisited.Clear();
            {
                foreach (var item2 in outerRect)
                {
                    if (!tmpWalls.Contains(item2))
                    {
                        FloorFillRoom(item2, tmpWalls, tmpVisited, parms.sketch, terrainDef, terrainDef, outerRect,
                            parms.singleFloorType ?? false);
                    }
                }

                return;
            }
        }

        var array = AbstractShapeGenerator.Generate(outerRect.Width, outerRect.Height, true, true);
        foreach (var item3 in outerRect)
        {
            if (parms.sketch.ThingsAt(item3).All(x => x.def.Fillage != FillCategory.Full) &&
                (array[item3.x - outerRect.minX, item3.z - outerRect.minZ] || (parms.singleFloorType ?? false)))
            {
                parms.sketch.AddTerrain(terrainDef, item3, false);
            }
        }
    }

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    private void FloorFillRoom(IntVec3 c, HashSet<IntVec3> walls, HashSet<IntVec3> visited, Sketch sketch,
        TerrainDef def1, TerrainDef def2, CellRect outerRect, bool singleFloorType)
    {
        if (visited.Contains(c))
        {
            return;
        }

        tmpCells.Clear();
        tmpStack.Clear();
        tmpStack.Push(new Pair<int, int>(c.x, c.z));
        visited.Add(c);
        var num = c.x;
        var num2 = c.x;
        var num3 = c.z;
        var num4 = c.z;
        while (tmpStack.Count != 0)
        {
            var pair = tmpStack.Pop();
            var first = pair.First;
            var second = pair.Second;
            tmpCells.Add(new IntVec3(first, 0, second));
            num = Mathf.Min(num, first);
            num2 = Mathf.Max(num2, first);
            num3 = Mathf.Min(num3, second);
            num4 = Mathf.Max(num4, second);
            if (first > outerRect.minX && !walls.Contains(new IntVec3(first - 1, 0, second)) &&
                !visited.Contains(new IntVec3(first - 1, 0, second)))
            {
                visited.Add(new IntVec3(first - 1, 0, second));
                tmpStack.Push(new Pair<int, int>(first - 1, second));
            }

            if (second > outerRect.minZ && !walls.Contains(new IntVec3(first, 0, second - 1)) &&
                !visited.Contains(new IntVec3(first, 0, second - 1)))
            {
                visited.Add(new IntVec3(first, 0, second - 1));
                tmpStack.Push(new Pair<int, int>(first, second - 1));
            }

            if (first < outerRect.maxX && !walls.Contains(new IntVec3(first + 1, 0, second)) &&
                !visited.Contains(new IntVec3(first + 1, 0, second)))
            {
                visited.Add(new IntVec3(first + 1, 0, second));
                tmpStack.Push(new Pair<int, int>(first + 1, second));
            }

            if (second >= outerRect.maxZ || walls.Contains(new IntVec3(first, 0, second + 1)) ||
                visited.Contains(new IntVec3(first, 0, second + 1)))
            {
                continue;
            }

            visited.Add(new IntVec3(first, 0, second + 1));
            tmpStack.Push(new Pair<int, int>(first, second + 1));
        }

        foreach (var intVec3 in tmpCells)
        {
            if (outerRect.IsOnEdge(intVec3))
            {
                return;
            }
        }

        var cellRect = CellRect.FromLimits(num, num3, num2, num4);
        var array = AbstractShapeGenerator.Generate(cellRect.Width, cellRect.Height, true, true);
        foreach (var pos in tmpCells)
        {
            if (sketch.ThingsAt(pos).Any(x =>
                    x.def.passability == Traversability.Impassable && x.def.Fillage == FillCategory.Full))
            {
                continue;
            }

            if (array[pos.x - cellRect.minX, pos.z - cellRect.minZ] || singleFloorType)
            {
                sketch.AddTerrain(def1, pos, false);
            }
            else
            {
                sketch.AddTerrain(def2, pos, false);
            }
        }
    }
}