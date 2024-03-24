using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_Courtyard : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var custom = parms.GetCustom<bool>("alleywayDone");
        var num = Rand.Range(0, 100);
        if (custom)
        {
            if (num <= 50)
            {
                TROMSketchResolverDefOf.CourtyardGarage.Resolve(parms);
            }
            else if (num <= 70)
            {
                TROMSketchResolverDefOf.CourtyardGarden.Resolve(parms);
            }
        }
        else if (num <= 20)
        {
            TROMSketchResolverDefOf.CourtyardGarden.Resolve(parms);
        }

        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms);
    }
}