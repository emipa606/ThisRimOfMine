using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM;

internal class SketchResolver_Street_Barricade : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        if (parms.GetCustom<Rot4>("direction") == Rot4.North)
        {
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 6), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 6), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(2, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(3, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(4, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(2, 0, 6), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(5, 0, 6), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(1, 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(3, 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(5, 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(6, 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(2, 4), 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(5, 7), 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(0, 2), 0, 0), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(4, 6), 0, 0), Rot4.North);
            parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
        }
        else if (parms.GetCustom<Rot4>("direction") == Rot4.South)
        {
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 0), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 0), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(2, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(3, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(4, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(2, 0, 0), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(5, 0, 0), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(1, 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(3, 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(5, 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(6, 0, 2), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(0, 2), 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(4, 6), 0, 4), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(2, 4), 0, 6), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(Rand.Range(5, 7), 0, 6), Rot4.North);
            parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
        }
        else if (parms.GetCustom<Rot4>("direction") == Rot4.East)
        {
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 6), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 1), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 2), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 3), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 4), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 5), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(5, 0, 6), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(6, 0, 2), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(6, 0, 5), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 1), Rot4.West);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 2), Rot4.West);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 3), Rot4.West);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 4), Rot4.West);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 5), Rot4.West);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(4, 0, 6), Rot4.West);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(0, 0, Rand.Range(0, 2)), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(0, 0, Rand.Range(4, 6)), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(2, 0, Rand.Range(2, 4)), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(2, 0, Rand.Range(5, 7)), Rot4.North);
            parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
        }
        else if (parms.GetCustom<Rot4>("direction") == Rot4.West)
        {
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 1), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 6), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 1), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 2), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 3), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 4), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 5), Rot4.East);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 6), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(0, 0, 2), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(0, 0, 5), Rot4.Random);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 1), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 2), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 3), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 4), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 5), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientRazorWire, new IntVec3(2, 0, 6), Rot4.East);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(5, 0, Rand.Range(0, 2)), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(5, 0, Rand.Range(4, 6)), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(3, 0, Rand.Range(2, 4)), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientTankTrap, new IntVec3(3, 0, Rand.Range(5, 7)), Rot4.North);
            parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
        }
    }
}