using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_HouseEdge_R11_1 : SketchResolver
{
    public static readonly int x = 11;

    public static readonly int z = 11;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        parms.rect = new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, x, z);
        var thingDef = Filters.RandStone();
        var custom = parms.GetCustom<bool>("backhouseAllow");
        var custom2 = parms.GetCustom<bool>("backhouseVariant");
        var sketch = new Sketch();
        var array = BoxShapeGenerator.Generate(5, 7);
        var array2 = new bool[11, 11];
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                if (!array[i, j])
                {
                    continue;
                }

                array2[i, j] = true;
                array2[i + 6, j] = true;
            }
        }

        if (custom)
        {
            if (custom2)
            {
                array2[2, 8] = true;
                array2[2, 9] = true;
                array2[2, 10] = true;
                array2[3, 10] = true;
                array2[4, 10] = true;
                array2[5, 10] = true;
                array2[6, 10] = true;
                array2[7, 10] = true;
                array2[8, 10] = true;
                array2[9, 10] = true;
                array2[10, 10] = true;
                array2[10, 9] = true;
                array2[10, 8] = true;
                array2[10, 7] = true;
            }
            else
            {
                array2[0, 7] = true;
                array2[0, 8] = true;
                array2[0, 9] = true;
                array2[0, 10] = true;
                array2[1, 10] = true;
                array2[2, 10] = true;
                array2[3, 10] = true;
                array2[4, 10] = true;
                array2[5, 10] = true;
                array2[6, 10] = true;
                array2[7, 10] = true;
                array2[8, 10] = true;
                array2[8, 8] = true;
                array2[8, 9] = true;
            }
        }

        array2[4, 3] = false;
        array2[6, 3] = false;
        for (var k = 0; k < array2.GetLength(0); k++)
        {
            for (var l = 0; l < array2.GetLength(1); l++)
            {
                if (array2[k, l])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(k, 0, l), Rot4.North, thingDef);
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
            parms3.rect = new CellRect(1, 1, 3, 5);
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            parms3.rect = new CellRect(7, 1, 3, 5);
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            if (custom)
            {
                parms3.rect = custom2 ? new CellRect(3, 7, 7, 3) : new CellRect(1, 7, 7, 3);

                TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            }

            parms3.rect = new CellRect(4, 0, 3, 7);
            parms3.thingCentral = thingDef;
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            if (custom)
            {
                parms3.rect = custom2 ? new CellRect(2, 7, 1, 1) : new CellRect(8, 7, 1, 1);

                parms3.thingCentral = thingDef;
                TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            }
        }

        var things = sketch.Things;
        foreach (var sketchThing in things)
        {
            if (sketchThing.def == ThingDefOf.Wall)
            {
                sketch.RemoveTerrain(sketchThing.pos);
            }
        }

        sketch.AddThing(ThingDefOf.Door, new IntVec3(4, 0, 3), Rot4.East);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(6, 0, 3), Rot4.East);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(5, 0, 1), Rot4.North);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(5, 0, 6), Rot4.North);
        if (custom)
        {
            sketch.AddThing(ThingDefOf.Door, custom2 ? new IntVec3(2, 0, 7) : new IntVec3(8, 0, 7), Rot4.East);
        }

        if (custom)
        {
            if (Rand.Bool)
            {
                sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(Rand.Range(1, 4), 0, 5), Rot4.South);
            }

            if (Rand.Bool)
            {
                sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(Rand.Range(7, 10), 0, 5), Rot4.South);
            }

            if (custom2)
            {
                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(9, 0, 8), Rot4.East);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientWashingMachine, new IntVec3(5, 0, 9), Rot4.North);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientToilet, new IntVec3(7, 0, 9), Rot4.North);
                }
            }
            else
            {
                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(7, 0, 8), Rot4.West);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientWashingMachine, new IntVec3(3, 0, 9), Rot4.North);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientToilet, new IntVec3(5, 0, 9), Rot4.North);
                }
            }
        }

        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms2);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}