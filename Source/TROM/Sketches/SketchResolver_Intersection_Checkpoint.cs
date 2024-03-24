using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_Intersection_Checkpoint : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(1, 0, 2), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(1, 0, 5), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(6, 0, 2), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(6, 0, 5), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(2, 0, 1), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(2, 0, 6), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(5, 0, 1), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(5, 0, 6), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 1), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 2), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 1), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(2, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(7, 0, 1), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(7, 0, 2), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 1), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 5), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 6), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 6), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 7), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(2, 0, 7), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(7, 0, 5), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(7, 0, 6), Rot4.West);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 6), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 7), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 7), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(2, 0, 2), Rot4.Random);
        sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(5, 0, 5), Rot4.Random);
        sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(2, 0, 5), Rot4.Random);
        sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(5, 0, 2), Rot4.Random);
        parms.sketch.MergeAt(sketch, default);
    }
}