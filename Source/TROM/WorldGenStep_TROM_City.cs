using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using Verse;

namespace TROM;

internal class WorldGenStep_TROM_City : WorldGenStep
{
    private readonly List<int> cities = [];

    public override int SeedPart => 349793029;

    public override void GenerateFresh(string seed)
    {
        GenerateCityCentral();
    }

    public int FindCityCentralTiles()
    {
        for (var i = 0; i < 500; i++)
        {
            if ((from _ in Enumerable.Range(0, 100)
                    select Rand.Range(0, Find.WorldGrid.TilesCount)).TryRandomElementByWeight(delegate(int x)
                {
                    var tile = Find.WorldGrid[x];
                    if (!tile.biome.canBuildBase || !tile.biome.implemented)
                    {
                        return 0f;
                    }

                    if (tile.hilliness != Hilliness.Flat)
                    {
                        return 0f;
                    }

                    if (!tile.biome.canAutoChoose)
                    {
                        return 0f;
                    }

                    return !tile.potentialRivers.NullOrEmpty() || !tile.potentialRoads.NullOrEmpty()
                        ? 0f
                        : tile.biome.settlementSelectionWeight;
                }, out var result) && IsValidTileForNewCity(result))
            {
                return result;
            }
        }

        return 0;
    }

    public bool IsValidTileForNewCity(int tile)
    {
        if (cities.Count == 0)
        {
            return true;
        }

        foreach (var secondTile in cities)
        {
            if (Find.WorldGrid.ApproxDistanceInTiles(tile, secondTile) < 10f)
            {
                return false;
            }
        }

        return true;
    }

    public void GenerateCityCentral()
    {
        var tiles = Find.WorldGrid.tiles;
        var num = (int)(1000f * Find.World.info.planetCoverage);
        var num2 = (int)(100f * Find.World.info.planetCoverage);
        var num3 = 0;
        for (var i = 0; i < num; i++)
        {
            if (num3 >= num2)
            {
                break;
            }

            var num4 = FindCityCentralTiles();
            var tile = tiles[num4];
            if (LoadedModManager.GetMod<TROMMod>().GetSettings<TROMSettings>().cityRestriction)
            {
                if (tile.temperature is <= -0f or >= 25f || tile.rainfall is <= 400f or >= 2000f ||
                    tile.elevation is >= 1000f or <= 20f)
                {
                    continue;
                }

                tile.biome = TROMBiomeDefOf.CityCentral;
                cities.Add(num4);
                GenerateCityArea(num4);
                num3++;
            }
            else
            {
                tile.biome = TROMBiomeDefOf.CityCentral;
                cities.Add(num4);
                GenerateCityArea(num4);
                num3++;
            }
        }
    }

    public void GenerateCityArea(int tile)
    {
        var list = new List<int>();
        Find.WorldGrid.GetTileNeighbors(tile, list);
        foreach (var index in list)
        {
            Find.WorldGrid.tiles[index].biome = TROMBiomeDefOf.CityOutskirts;
        }
    }
}