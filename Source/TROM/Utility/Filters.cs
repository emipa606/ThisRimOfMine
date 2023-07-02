using RimWorld;
using Verse;

namespace TROM.Utility;

public static class Filters
{
    public static ThingDef RandStone()
    {
        var num = Rand.Range(0, 100);
        switch (num)
        {
            case <= 20:
                return ThingDefOf.BlocksGranite;
            case <= 40:
                return TROMThingDefOf.BlocksLimestone;
            case <= 60:
                return TROMThingDefOf.BlocksMarble;
            case <= 80:
                return TROMThingDefOf.BlocksSandstone;
            default:
                return TROMThingDefOf.BlocksSlate;
        }
    }
}