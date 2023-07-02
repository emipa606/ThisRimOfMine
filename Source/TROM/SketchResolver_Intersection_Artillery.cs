using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM;

internal class SketchResolver_Intersection_Artillery : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        if (parms.rect != null)
        {
            var array = new bool[parms.rect.Value.Width, parms.rect.Value.Height];
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    if (i is 0 or 7 && j is >= 2 and <= 5 || j is 0 or 7 && i is >= 2 and <= 5)
                    {
                        array[i, j] = true;
                    }
                    else
                    {
                        array[i, j] = false;
                    }
                }
            }

            for (var k = 0; k < array.GetLength(0); k++)
            {
                for (var l = 0; l < array.GetLength(1); l++)
                {
                    if (!array[k, l] || Rand.Range(0, 100) <= 20)
                    {
                        continue;
                    }

                    if (k is 0 or 7)
                    {
                        sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(k, 0, l), Rot4.East);
                    }

                    if (l is 0 or 7)
                    {
                        sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(k, 0, l), Rot4.North);
                    }
                }
            }
        }

        sketch.AddThing(ThingDefOf.AncientMegaCannonTripod, new IntVec3(4, 0, 4), Rot4.Random);
        if (Rand.Range(0, 100) > 50)
        {
            sketch.AddThing(ThingDefOf.AncientMegaCannonBarrel, new IntVec3(2, 0, 2), Rot4.Random);
        }

        parms.sketch.MergeAt(sketch, default);
    }
}