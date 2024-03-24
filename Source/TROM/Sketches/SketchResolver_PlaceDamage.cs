using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_PlaceDamage : SketchResolver
{
    private float damageScaleNormal;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var Furniture = Common.Furniture;
        if (parms.monumentSize == null)
        {
            return;
        }

        var value = parms.monumentSize.Value;
        var x = value.x;
        var z = value.z;
        var sketch = new Sketch();
        damageScaleNormal = parms.GetCustom<float>("damageScaleNormal");
        var list = new List<SketchThing>();
        var list2 = new List<SketchThing>();
        foreach (var item in parms.sketch.Things.ToList())
        {
            if (item.def == ThingDefOf.Wall)
            {
                list.Add(item);
            }
            else if (IsSketchThingFurniture(item.def))
            {
                list2.Add(item);
            }
        }

        foreach (var item2 in list)
        {
            if (!(Rand.Range(0f, 1f) >= damageScaleNormal))
            {
                continue;
            }

            item2.hitPoints =
                (int)(item2.MaxHitPoints * Rand.Range(damageScaleNormal - 0.1f, damageScaleNormal + 0.1f));
            if (item2.hitPoints <= item2.MaxHitPoints * 0.75f)
            {
                var newX = Rand.Range(Math.Max(0, item2.pos.x - 1), Math.Min(item2.pos.x + 1, x));
                var newZ = Rand.Range(Math.Max(0, item2.pos.z - 1), Math.Min(item2.pos.z + 1, z));
                sketch.AddThing(ThingDefOf.Filth_RubbleBuilding, new IntVec3(newX, 0, newZ), Rot4.Random);
            }

            if (!(item2.hitPoints <= item2.MaxHitPoints * 0.5f))
            {
                continue;
            }

            var newX2 = Rand.Range(Math.Max(0, item2.pos.x - 1), Math.Min(item2.pos.x + 1, x));
            var newZ2 = Rand.Range(Math.Max(0, item2.pos.z - 1), Math.Min(item2.pos.z + 1, z));
            sketch.AddThing(ThingDefOf.Filth_RubbleBuilding, new IntVec3(newX2, 0, newZ2), Rot4.Random);
        }

        var num = damageScaleNormal - 0.5f;
        foreach (var item3 in list2)
        {
            if (!(Rand.Range(0f, 1f) >= damageScaleNormal - 0.2f))
            {
                continue;
            }

            item3.hitPoints = Math.Max((int)(item3.MaxHitPoints * Rand.Range(num - 0.1f, num + 0.1f)), 1);
            if (item3.hitPoints <= item3.MaxHitPoints * 0.75f)
            {
                var newX3 = Rand.Range(Math.Max(0, item3.pos.x - 1), Math.Min(item3.pos.x + 1, x));
                var newZ3 = Rand.Range(Math.Max(0, item3.pos.z - 1), Math.Min(item3.pos.z + 1, z));
                sketch.AddThing(ThingDefOf.Filth_RubbleBuilding, new IntVec3(newX3, 0, newZ3), Rot4.Random);
            }

            if (item3.hitPoints <= item3.MaxHitPoints * 0.5f)
            {
                var newX4 = Rand.Range(Math.Max(0, item3.pos.x - 1), Math.Min(item3.pos.x + 1, x));
                var newZ4 = Rand.Range(Math.Max(0, item3.pos.z - 1), Math.Min(item3.pos.z + 1, z));
                sketch.AddThing(ThingDefOf.Filth_RubbleBuilding, new IntVec3(newX4, 0, newZ4), Rot4.Random);
            }

            if (!(item3.hitPoints <= item3.MaxHitPoints * 0.05f))
            {
                continue;
            }

            var list3 = item3.OccupiedRect.ToList();
            foreach (var item4 in list3)
            {
                sketch.AddThing(ThingDefOf.Filth_RubbleBuilding, item4, Rot4.Random);
            }

            if (Rand.Bool)
            {
                parms.sketch.Remove(item3);
            }
        }

        parms.sketch.MergeAt(sketch, default);

        return;

        bool IsSketchThingFurniture(ThingDef st)
        {
            return Enumerable.Contains(Furniture, st);
        }
    }
}