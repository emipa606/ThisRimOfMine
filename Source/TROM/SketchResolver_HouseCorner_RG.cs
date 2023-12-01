using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_HouseCorner_RG : SketchResolver
{
    public static readonly int x = 11;

    public static readonly int z = 7;

    public static readonly List<CellRect> roofList = [new CellRect(0, 0, 11, 7)];

    private bool switche;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        switche = false;
        var stuff = Filters.RandStone();
        parms.rect = !switche
            ? new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, x, z)
            : new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, z, x);

        var sketch = new Sketch();
        var width = parms.rect.Value.Width;
        var height = parms.rect.Value.Height;
        bool[,] array;
        if (!switche)
        {
            array = BoxShapeGenerator.Generate(width, height);
            array[5, 1] = true;
            array[5, 2] = true;
            array[5, 3] = true;
            array[5, 4] = true;
            array[5, 5] = true;
        }
        else
        {
            array = BoxShapeGenerator.Generate(width, height);
            array[1, 5] = true;
            array[2, 5] = true;
            array[3, 5] = true;
            array[4, 5] = true;
            array[5, 5] = true;
        }

        if (!switche)
        {
            array[0, 3] = false;
            array[8, 0] = false;
        }
        else
        {
            array[3, 0] = false;
            array[0, 8] = false;
        }

        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(i, 0, j), Rot4.North, stuff);
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
        for (var k = 0; k < things.Count; k++)
        {
            if (things[k].def == ThingDefOf.Wall)
            {
                sketch.RemoveTerrain(things[k].pos);
            }
        }

        if (!switche)
        {
            sketch.AddThing(ThingDefOf.Door, new IntVec3(0, 0, 3), Rot4.East, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.Door, new IntVec3(8, 0, 0), Rot4.North, ThingDefOf.WoodLog);
        }
        else
        {
            sketch.AddThing(ThingDefOf.Door, new IntVec3(3, 0, 0), Rot4.North, ThingDefOf.WoodLog);
            sketch.AddThing(ThingDefOf.Door, new IntVec3(0, 0, 8), Rot4.East, ThingDefOf.WoodLog);
        }

        if (!switche)
        {
            if (Rand.Range(0, 100) > 30)
            {
                sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(4, 0, 1), Rot4.North);
            }

            if (Rand.Range(0, 100) > 30)
            {
                sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(9, 0, 5), Rot4.West);
            }
        }
        else
        {
            if (Rand.Range(0, 100) > 30)
            {
                sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(5, 0, 4), Rot4.West);
            }

            if (Rand.Range(0, 100) > 30)
            {
                sketch.AddThing(ThingDefOf.AncientBed, new IntVec3(5, 0, 9), Rot4.West);
            }
        }

        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms2);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}