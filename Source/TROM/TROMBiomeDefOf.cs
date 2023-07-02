using RimWorld;

namespace TROM;

[DefOf]
public static class TROMBiomeDefOf
{
    public static BiomeDef CityOutskirts;

    public static BiomeDef CityCentral;

    static TROMBiomeDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BiomeDefOf));
    }
}