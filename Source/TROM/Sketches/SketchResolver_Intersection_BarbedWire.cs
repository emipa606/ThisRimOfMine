using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_Intersection_BarbedWire : SketchResolver
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
                    if (i is 1 or 6 && j is >= 2 and <= 5 || j is 1 or 6 && i is >= 2 and <= 5)
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

                    if (k is 1 or 6)
                    {
                        sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(k, 0, l), Rot4.East);
                    }

                    if (l is 1 or 6)
                    {
                        sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(k, 0, l), Rot4.North);
                    }
                }
            }
        }

        sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(Rand.Range(2, 6), 0, Rand.Range(2, 6)),
            Rot4.Random);
        parms.sketch.MergeAt(sketch, default);
    }
}