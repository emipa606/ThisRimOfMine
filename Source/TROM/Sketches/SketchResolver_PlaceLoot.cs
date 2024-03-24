using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_PlaceLoot : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        var list = parms.GetCustom<List<ThingDef>>("allowedContainers") ?? [TROMThingDefOf.Shelf];
        float obj = parms.TryGetCustom("chance", out obj) ? obj : 0.1f;
        float scaling = parms.TryGetCustom("scaling", out scaling) ? scaling : 1f;
        var custom = parms.GetCustom<Loot.lootType>("lootType");
        var value = parms.rect.Value;
        var list2 = new List<SketchThing>();
        var list3 = new List<IntVec3>();
        foreach (var item in parms.sketch.Things.ToList())
        {
            if (value.Contains(item.pos) && list.Contains(item.def))
            {
                list2.Add(item);
            }
        }

        foreach (var item2 in list2)
        {
            for (var i = 0; i < item2.OccupiedRect.Cells.Count(); i++)
            {
                list3.Add(item2.OccupiedRect.Cells.ElementAt(i));
            }
        }

        foreach (var item3 in list3)
        {
            if (!(Rand.Range(0f, 1f) <= obj))
            {
                continue;
            }

            var thingDef = Loot.Generate(custom);
            sketch.AddThing(thingDef, item3, Rot4.North, null, calculateStackCount(custom, thingDef));
        }

        parms.sketch.MergeAt(sketch, new IntVec3(0, 0, 0));
        return;

        int calculateStackCount(Loot.lootType lt2, ThingDef lootThing2)
        {
            if (lt2 == Loot.lootType.FoodGreens)
            {
                return (int)(Rand.Range(0.2f * scaling, 0.5f * scaling) /
                             lootThing2.GetStatValueAbstract(StatDefOf.Nutrition));
            }

            return 1;
        }
    }
}