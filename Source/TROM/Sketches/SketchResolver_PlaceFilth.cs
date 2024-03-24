using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_PlaceFilth : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        var list = new List<SketchThing>();
        foreach (var item in parms.sketch.Things.ToList())
        {
            if (item.def == ThingDefOf.Sandbags || item.def == ThingDefOf.AncientRustedCar ||
                item.def == TROMThingDefOf.AncientRustedTruck)
            {
                list.Add(item);
            }
        }

        foreach (var item2 in list)
        {
            if (item2.def == ThingDefOf.Sandbags)
            {
                sketch.AddThing(TROMThingDefOf.SandbagRubble,
                    new IntVec3(Rand.Element(item2.pos.x + 1, item2.pos.x - 1), 0,
                        Rand.Element(item2.pos.z + 1, item2.pos.z - 1)), Rot4.Random);
                sketch.AddThing(TROMThingDefOf.Filth_Sand,
                    new IntVec3(Rand.Element(item2.pos.x + 1, item2.pos.x - 1), 0,
                        Rand.Element(item2.pos.z + 1, item2.pos.z - 1)), Rot4.Random);
                sketch.AddThing(TROMThingDefOf.Filth_Sand,
                    new IntVec3(Rand.Element(item2.pos.x + 1, item2.pos.x - 1), 0,
                        Rand.Element(item2.pos.z + 1, item2.pos.z - 1)), Rot4.Random);
            }
            else if (item2.def == ThingDefOf.AncientBarrel || item2.def == TROMThingDefOf.CrateBarrel)
            {
                sketch.AddThing(ThingDefOf.Filth_Fuel,
                    new IntVec3(Rand.Element(item2.pos.x + 1, item2.pos.x - 1), 0,
                        Rand.Element(item2.pos.z + 1, item2.pos.z - 1)), Rot4.Random);
            }
            else if (item2.def == ThingDefOf.AncientRustedCar || item2.def == TROMThingDefOf.AncientRustedTruck)
            {
                var intVec = item2.OccupiedRect.ToList().RandomElement();
                sketch.AddThing(TROMThingDefOf.SlagRubble,
                    new IntVec3(Rand.Element(intVec.x + 1, intVec.x - 1), 0, Rand.Element(intVec.z + 1, intVec.z - 1)),
                    Rot4.Random);
            }
        }

        parms.sketch.MergeAt(sketch, default);
    }
}