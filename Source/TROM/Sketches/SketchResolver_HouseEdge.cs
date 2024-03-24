using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_HouseEdge : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        parms.rect = new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, parms.rect.Value.Width,
            parms.rect.Value.Height);
        var thingDef = Filters.RandStone();
        var width = parms.rect.Value.Width;
        var height = parms.rect.Value.Height;
        var num = Rand.Range(1, width - 2);
        var sketch = new Sketch();
        var array = BoxShapeGenerator.Generate(width, height);
        array[num, 0] = false;
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(i, 0, j), Rot4.North, thingDef);
                }
            }
        }

        var parms2 = parms;
        parms2.sketch = sketch;
        parms2.connectedGroupsSameStuff = true;
        if (parms.addFloors ?? true)
        {
            var parms3 = parms;
            parms3.thingCentral = ThingDefOf.WoodLog;
            parms3.singleFloorType = true;
            parms3.sketch = sketch;
            parms3.floorFillRoomsOnly = false;
            parms3.onlyStoneFloors = parms.onlyStoneFloors ?? false;
            parms3.allowConcrete = parms.allowConcrete ?? false;
            parms3.rect = new CellRect(1, 1, width - 2, height - 2);
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            parms3.rect = new CellRect(num, 0, 1, 1);
            parms3.thingCentral = thingDef;
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
        }

        var things = sketch.Things;
        foreach (var sketchThing in things)
        {
            if (sketchThing.def == ThingDefOf.Wall)
            {
                sketch.RemoveTerrain(sketchThing.pos);
            }
        }

        sketch.AddThing(ThingDefOf.Door, new IntVec3(num, 0, 0), Rot4.North);
        if (Rand.Bool)
        {
            sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(Rand.Range(1, width - 2), 0, height - 3), Rot4.North);
        }

        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms2);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}