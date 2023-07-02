using RimWorld;
using Verse;

namespace TROM;

[DefOf]
public static class TROMTerrainDefOf
{
    public static TerrainDef Asphalt;

    public static TerrainDef BrokenAsphalt;

    public static TerrainDef FlagstoneGranite;

    public static TerrainDef FlagstoneLimestone;

    public static TerrainDef FlagstoneMarble;

    public static TerrainDef FlagstoneSlate;

    public static TerrainDef Pavement;

    public static TerrainDef TileGranite;

    public static TerrainDef TileLimestone;

    public static TerrainDef TileMarble;

    public static TerrainDef TileSlate;

    static TROMTerrainDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(TerrainDefOf));
    }
}