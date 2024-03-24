using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_HouseEdge_R7_Parking : SketchResolver
{
    public static readonly int x = 7;

    public static readonly int z = 7;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        if (parms.rect != null)
        {
            parms.rect = new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, x, z);
            var sketch = new Sketch();
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < z; j++)
                {
                    sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(i, 0, j));
                    if (j == 6)
                    {
                        sketch.AddThing(ThingDef.Named("Fence"), new IntVec3(i, 0, 6), Rot4.North, ThingDefOf.Steel);
                    }
                }
            }

            if (Rand.Bool)
            {
                sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(2, 0, 0), Rot4.West);
            }

            if (Rand.Bool)
            {
                sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(2, 0, 2), Rot4.West);
            }

            if (Rand.Bool)
            {
                sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(2, 0, 4), Rot4.West);
            }

            parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
        }

        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
    }
}