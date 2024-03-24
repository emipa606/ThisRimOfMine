using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_HouseAlleyway : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        if (parms.rect == null)
        {
            return;
        }

        parms.rect = new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, 3, 11);
        var sketch = new Sketch();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 11; j++)
            {
                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(i, 0, j));
            }
        }

        sketch.AddThing(TROMThingDefOf.AncientVendingMachine, new IntVec3(0, 0, Rand.Range(0, 3)), Rot4.West);
        sketch.AddThing(ThingDefOf.AncientBarrel, new IntVec3(0, 0, Rand.Range(3, 7)), Rot4.North);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}