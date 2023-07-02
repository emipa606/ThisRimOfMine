using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_HouseCorner_R1 : SketchResolver
{
    public static readonly int x = 11;

    public static readonly int z = 11;

    public static readonly List<CellRect> roofList = new List<CellRect>
    {
        new CellRect(0, 4, 11, 7),
        new CellRect(1, 3, 10, 1),
        new CellRect(2, 2, 9, 1),
        new CellRect(3, 1, 8, 1),
        new CellRect(4, 0, 7, 1)
    };

    private bool switche;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        parms.rect = new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, x, z);
        var stuff = Filters.RandStone();
        var width = parms.rect.Value.Width;
        var height = parms.rect.Value.Height;
        var sketch = new Sketch();
        var array = BoxShapeGenerator.Generate(width, height);
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                if (i + j == 4)
                {
                    array[i, j] = true;
                }
                else
                {
                    array[i, j] = false;
                }
            }
        }

        if (Rand.Range(0, 100) > 50)
        {
            array[6, 1] = true;
            array[6, 2] = true;
            array[6, 3] = true;
            array[6, 5] = true;
            array[6, 6] = true;
            array[6, 7] = true;
            array[6, 9] = true;
            array[1, 6] = true;
            array[2, 6] = true;
            array[3, 6] = true;
            array[4, 6] = true;
            array[5, 6] = true;
            array[8, 0] = false;
            switche = false;
        }
        else
        {
            array[1, 6] = true;
            array[2, 6] = true;
            array[3, 6] = true;
            array[5, 6] = true;
            array[6, 6] = true;
            array[7, 6] = true;
            array[9, 6] = true;
            array[6, 1] = true;
            array[6, 2] = true;
            array[6, 3] = true;
            array[6, 4] = true;
            array[6, 5] = true;
            array[0, 8] = false;
            switche = true;
        }

        for (var k = 0; k < array.GetLength(0); k++)
        {
            for (var l = 0; l < array.GetLength(1); l++)
            {
                if (array[k, l])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(k, 0, l), Rot4.North, stuff);
                }
            }
        }

        var parms2 = parms;
        parms2.sketch = sketch;
        parms2.connectedGroupsSameStuff = true;
        if (parms.addFloors ?? true)
        {
            var parms3 = parms;
            parms3.singleFloorType = true;
            parms3.sketch = sketch;
            parms3.floorFillRoomsOnly = false;
            parms3.onlyStoneFloors = parms.onlyStoneFloors ?? true;
            parms3.allowConcrete = parms.allowConcrete ?? false;
            parms3.rect = new CellRect(0, 0, width, height);
            SketchResolverDefOf.FloorFill.Resolve(parms3);
        }

        var things = sketch.Things;
        for (var m = 0; m < things.Count; m++)
        {
            if (things[m].def == ThingDefOf.Wall)
            {
                sketch.RemoveTerrain(things[m].pos);
            }
        }

        if (!switche)
        {
            sketch.AddThing(ThingDefOf.Door, new IntVec3(8, 0, 0), Rot4.North, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.Door, new IntVec3(6, 0, 4), Rot4.East, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.Door, new IntVec3(6, 0, 8), Rot4.East, ThingDefOf.WoodLog);
        }
        else
        {
            sketch.AddThing(ThingDefOf.Door, new IntVec3(0, 0, 8), Rot4.East, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.Door, new IntVec3(4, 0, 6), Rot4.North, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.Door, new IntVec3(8, 0, 6), Rot4.North, ThingDefOf.WoodLog);
        }

        if (!switche)
        {
            sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(1, 0, 8), Rot4.East);
            if (Rand.Range(0, 100) > 99)
            {
                sketch.AddThing(TROMThingDefOf.TubeTelevision, new IntVec3(3, 0, 2), Rot4.North);
            }

            if (Rand.Range(0, 100) > 60)
            {
                sketch.AddThing(TROMThingDefOf.Armchair, new IntVec3(4, 0, 5), Rot4.South);
            }

            if (Rand.Range(0, 100) > 95)
            {
                sketch.AddThing(ThingDefOf.AncientLamp, new IntVec3(5, 0, 3), Rot4.North);
            }

            sketch.AddThing(ThingDefOf.PlantPot, new IntVec3(5, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Table1x2c, new IntVec3(1, 0, 4), Rot4.North, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(2, 0, 4), Rot4.West, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(2, 0, 5), Rot4.West, ThingDefOf.WoodLog);
            sketch.AddThing(TROMThingDefOf.AncientStove, new IntVec3(9, 0, 6), Rot4.East);
            sketch.AddThing(TROMThingDefOf.AncientOven, new IntVec3(9, 0, 7), Rot4.East);
            sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(9, 0, 5), Rot4.East);
        }
        else
        {
            sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(8, 0, 1), Rot4.North);
            if (Rand.Range(0, 100) > 98)
            {
                sketch.AddThing(TROMThingDefOf.TubeTelevision, new IntVec3(2, 0, 3), Rot4.East);
            }

            if (Rand.Range(0, 100) > 60)
            {
                sketch.AddThing(TROMThingDefOf.Armchair, new IntVec3(5, 0, 4), Rot4.West);
            }

            if (Rand.Range(0, 100) > 95)
            {
                sketch.AddThing(ThingDefOf.AncientLamp, new IntVec3(3, 0, 5), Rot4.North);
            }

            sketch.AddThing(ThingDefOf.PlantPot, new IntVec3(5, 0, 5), Rot4.North);
            sketch.AddThing(ThingDefOf.Table1x2c, new IntVec3(4, 0, 1), Rot4.East, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(4, 0, 2), Rot4.South, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(5, 0, 2), Rot4.South, ThingDefOf.WoodLog);
            sketch.AddThing(TROMThingDefOf.AncientStove, new IntVec3(6, 0, 9), Rot4.North);
            sketch.AddThing(TROMThingDefOf.AncientOven, new IntVec3(7, 0, 9), Rot4.North);
            sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(5, 0, 9), Rot4.North);
        }

        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms2);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}