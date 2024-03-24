using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_HouseCorner_R2 : SketchResolver
{
    public static readonly int x = 11;

    public static readonly int z = 11;

    public static readonly List<CellRect> roofList =
    [
        new CellRect(0, 4, 11, 7),
        new CellRect(4, 0, 7, 4)
    ];

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
        var array = new bool[11, 11];
        var array2 = BoxShapeGenerator.Generate(width, 7);
        var array3 = BoxShapeGenerator.Generate(7, height);
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < 7; j++)
            {
                if (array2[i, j])
                {
                    array[i, j + 4] = true;
                }
            }
        }

        for (var k = 0; k < 7; k++)
        {
            for (var l = 0; l < height; l++)
            {
                if (array3[k, l])
                {
                    array[k + 4, l] = true;
                }
            }
        }

        array[6, 0] = false;
        array[0, 6] = false;
        array[4, 8] = false;
        array[8, 4] = false;
        for (var m = 0; m < array.GetLength(0); m++)
        {
            for (var n = 0; n < array.GetLength(1); n++)
            {
                if (array[m, n])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(m, 0, n), Rot4.North, stuff);
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
        foreach (var sketchThing in things)
        {
            if (sketchThing.def == ThingDefOf.Wall)
            {
                sketch.RemoveTerrain(sketchThing.pos);
            }
        }

        sketch.AddThing(ThingDefOf.Door, new IntVec3(6, 0, 0), Rot4.North, ThingDefOf.WoodLog);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(0, 0, 6), Rot4.East, ThingDefOf.WoodLog);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(4, 0, 8), Rot4.East, ThingDefOf.WoodLog);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(8, 0, 4), Rot4.North, ThingDefOf.WoodLog);
        if (Rand.Range(0, 100) > 30)
        {
            sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(5, 0, 3), Rot4.East);
        }

        if (Rand.Range(0, 100) > 50)
        {
            sketch.AddThing(ThingDef.Named("PlantPot"), new IntVec3(9, 0, 3), Rot4.North);
        }

        if (Rand.Range(0, 100) > 30)
        {
            sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(3, 0, 5), Rot4.North);
        }

        if (Rand.Range(0, 100) > 60)
        {
            sketch.AddThing(ThingDef.Named("PlantPot"), new IntVec3(3, 0, 9), Rot4.North);
        }

        if (Rand.Range(0, 100) > 90)
        {
            sketch.AddThing(TROMThingDefOf.ChessTable, new IntVec3(8, 0, 8), Rot4.North);
        }

        if (Rand.Range(0, 100) > 60)
        {
            sketch.AddThing(TROMThingDefOf.Armchair, new IntVec3(8, 0, 9), Rot4.South);
        }

        if (Rand.Range(0, 100) > 60)
        {
            sketch.AddThing(TROMThingDefOf.Armchair, new IntVec3(9, 0, 8), Rot4.West);
        }

        if (Rand.Range(0, 100) > 95)
        {
            sketch.AddThing(ThingDefOf.AncientLamp, new IntVec3(7, 0, 7), Rot4.North);
        }

        if (Rand.Range(0, 100) > 50)
        {
            sketch.AddThing(ThingDefOf.Table1x2c, new IntVec3(5, 0, 5), Rot4.North, ThingDefOf.WoodLog);
        }

        if (Rand.Range(0, 100) > 20)
        {
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(6, 0, 5), Rot4.West, ThingDefOf.WoodLog);
        }

        if (Rand.Range(0, 100) > 75)
        {
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(6, 0, 6), Rot4.West, ThingDefOf.WoodLog);
        }

        if (Rand.Range(0, 100) > 75)
        {
            sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(5, 0, 7), Rot4.South, ThingDefOf.WoodLog);
        }

        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms2);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}