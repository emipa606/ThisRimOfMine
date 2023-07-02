using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_CourtyardGarage : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        var custom = parms.GetCustom<int>("AL");
        var custom2 = parms.GetCustom<int>("AR");
        var custom3 = parms.GetCustom<int>("AW");
        var custom5 = parms.GetCustom<Map>("map");
        var custom6 = parms.GetCustom<IntVec3>("b");
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 16; j++)
            {
                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(i + custom3, 0, j + 11));
            }
        }

        var num = custom3 - 11;
        var num2 = 38 - custom3 - 3 - 11;
        var num3 = 0;
        var num4 = Rand.Range(7, 10);
        var num5 = 0;
        var intVec = default(IntVec3);
        var generate = false;
        var array = new bool[1, 1];
        var thingDef = Filters.RandStone();
        if (num < num2)
        {
            if (num > 5)
            {
                generate = true;
                num3 = Rand.Range(5, num);
                num5 = custom;
                intVec = new IntVec3(Rand.Range(1, num3 - 1), 0, num4 - 1);
                array = BoxShapeGenerator.Generate(num3, num4);
            }
        }
        else if (num2 > 5)
        {
            generate = true;
            num3 = Rand.Range(5, num2);
            num5 = custom2;
            intVec = new IntVec3(Rand.Range(1, num3 - 1), 0, num4 - 1);
            array = BoxShapeGenerator.Generate(num3, num4);
        }

        if (generate)
        {
            if (num < num2)
            {
                array[intVec.x, intVec.z] = false;
                for (var k = 0; k < num3; k++)
                {
                    for (var l = 0; l < num4; l++)
                    {
                        if (array[k, l])
                        {
                            sketch.AddThing(ThingDefOf.Wall, new IntVec3(k + custom3 - num3, 0, l + num5), Rot4.North,
                                thingDef);
                        }
                        else if (l != num4 - 1)
                        {
                            sketch.AddTerrain(TerrainDefOf.WoodPlankFloor,
                                new IntVec3(k + custom3 - num3, 0, l + num5));
                        }
                    }
                }

                parms.rect = new CellRect(intVec.x + custom3 - num3, intVec.z + num5, 1, 1);
                parms.thingCentral = thingDef;
                TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms);
                sketch.AddThing(ThingDefOf.Door, new IntVec3(intVec.x + custom3 - num3, 0, intVec.z + num5),
                    Rot4.North);
            }
            else
            {
                array[intVec.x, intVec.z] = false;
                for (var m = 0; m < num3; m++)
                {
                    for (var n = 0; n < num4; n++)
                    {
                        if (array[m, n])
                        {
                            sketch.AddThing(ThingDefOf.Wall, new IntVec3(m + custom3 + 3, 0, n + num5), Rot4.North,
                                thingDef);
                        }
                        else if (n != num4 - 1)
                        {
                            sketch.AddTerrain(TerrainDefOf.WoodPlankFloor, new IntVec3(m + custom3 + 3, 0, n + num5));
                        }
                    }
                }

                parms.rect = new CellRect(intVec.x + custom3 + 3, intVec.z + num5, 1, 1);
                parms.thingCentral = thingDef;
                TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms);
                sketch.AddThing(ThingDefOf.Door, new IntVec3(intVec.x + custom3 + 3, 0, intVec.z + num5), Rot4.North);
            }
        }

        var array2 = new bool[6, 13];
        var num6 = Rand.Range(0, 4);
        var stuff = Filters.RandStone();
        if (num < num2)
        {
            array2 = ShapeGenerator.zLine(array2, new IntVec2(5, 0), 13);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(0, 0), 5);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(0, 4), 5);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(0, 8), 5);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(0, 12), 5);
            for (var num7 = 0; num7 < 6; num7++)
            {
                for (var num8 = 0; num8 < 13; num8++)
                {
                    if (array2[num7, num8])
                    {
                        sketch.AddThing(ThingDefOf.Wall, new IntVec3(num7 + custom3 + 3, 0, num8 + num6 + 11),
                            Rot4.North, stuff);
                        continue;
                    }

                    if (num7 == 0)
                    {
                        sketch.AddThing(TROMThingDefOf.AnimalFlap, new IntVec3(num7 + custom3 + 3, 0, num8 + num6 + 11),
                            Rot4.East);
                    }

                    sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(num7 + custom3 + 3, 0, num8 + num6 + 11));
                }
            }
        }
        else
        {
            array2 = ShapeGenerator.zLine(array2, new IntVec2(0, 0), 13);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(1, 0), 5);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(1, 4), 5);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(1, 8), 5);
            array2 = ShapeGenerator.xLine(array2, new IntVec2(1, 12), 5);
            for (var num9 = 0; num9 < 6; num9++)
            {
                for (var num10 = 0; num10 < 13; num10++)
                {
                    if (array2[num9, num10])
                    {
                        sketch.AddThing(ThingDefOf.Wall, new IntVec3(num9 + custom3 - 6, 0, num10 + num6 + 11),
                            Rot4.North, stuff);
                        continue;
                    }

                    if (num9 == 5)
                    {
                        sketch.AddThing(TROMThingDefOf.AnimalFlap,
                            new IntVec3(num9 + custom3 - 6, 0, num10 + num6 + 11), Rot4.East);
                    }

                    sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(num9 + custom3 - 6, 0, num10 + num6 + 11));
                }
            }
        }

        var addStuff = false;
        var num11 = Rand.Range(1, 4);
        if (Rand.Range(0, 100) <= 2)
        {
            addStuff = true;
        }

        if (num < num2)
        {
            for (var num12 = 1; num12 < 4; num12++)
            {
                if (addStuff && num11 == num12)
                {
                    sketch.AddThing(TROMThingDefOf.DrugLab, new IntVec3(custom3 + 6, 0, (num12 * 4) + num6 + 10),
                        Rot4.North, ThingDefOf.Steel);
                    sketch.AddThing(TROMThingDefOf.HydroponicsBasin,
                        new IntVec3(custom3 + 5, 0, (num12 * 4) + num6 + 8), Rot4.East);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 + 4, 0, (num12 * 4) + num6 + 8),
                        Rot4.North);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 + 5, 0, (num12 * 4) + num6 + 8),
                        Rot4.North);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 + 6, 0, (num12 * 4) + num6 + 8),
                        Rot4.North);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 + 7, 0, (num12 * 4) + num6 + 8),
                        Rot4.North);
                }
                else if (Rand.Range(0, 100) <= 70)
                {
                    sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(custom3 + 5, 0, (num12 * 4) + num6 + 10),
                        Rot4.East);
                }
            }
        }
        else
        {
            for (var num13 = 1; num13 < 4; num13++)
            {
                if (addStuff && num11 == num13)
                {
                    sketch.AddThing(TROMThingDefOf.DrugLab, new IntVec3(custom3 - 4, 0, (num13 * 4) + num6 + 10),
                        Rot4.North, ThingDefOf.Steel);
                    sketch.AddThing(TROMThingDefOf.HydroponicsBasin,
                        new IntVec3(custom3 - 4, 0, (num13 * 4) + num6 + 8), Rot4.East);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 - 5, 0, (num13 * 4) + num6 + 8),
                        Rot4.North);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 - 4, 0, (num13 * 4) + num6 + 8),
                        Rot4.North);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 - 3, 0, (num13 * 4) + num6 + 8),
                        Rot4.North);
                    sketch.AddThing(TROMThingDefOf.Plant_Psychoid, new IntVec3(custom3 - 2, 0, (num13 * 4) + num6 + 8),
                        Rot4.North);
                }
                else if (Rand.Range(0, 100) <= 70)
                {
                    sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(custom3 - 4, 0, (num13 * 4) + num6 + 10),
                        Rot4.East);
                }
            }
        }

        if (generate)
        {
            var array3 = BoxShapeGenerator.Generate(4, 5);
            var stuff2 = Filters.RandStone();
            if (num < num2)
            {
                array3[3, 2] = false;
                for (var num14 = 0; num14 < array3.GetLength(0); num14++)
                {
                    for (var num15 = 0; num15 < array3.GetLength(1); num15++)
                    {
                        if (array3[num14, num15])
                        {
                            sketch.AddThing(ThingDefOf.Wall,
                                new IntVec3(num14 + custom3 - array3.GetLength(0), 0, num15 + num5 + num4 + 2),
                                Rot4.North, stuff2);
                        }
                        else
                        {
                            sketch.AddTerrain(TerrainDefOf.Concrete,
                                new IntVec3(num14 + custom3 - array3.GetLength(0), 0, num15 + num5 + num4 + 2));
                        }
                    }
                }

                sketch.AddThing(ThingDefOf.Door, new IntVec3(custom3 - 1, 0, num5 + num4 + 4), Rot4.East,
                    ThingDefOf.Steel);
                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.Battery, new IntVec3(custom3 - 2, 0, num5 + num4 + 3), Rot4.West);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.Battery, new IntVec3(custom3 - 2, 0, num5 + num4 + 5), Rot4.West);
                }
            }
            else
            {
                array3[0, 2] = false;
                for (var num16 = 0; num16 < array3.GetLength(0); num16++)
                {
                    for (var num17 = 0; num17 < array3.GetLength(1); num17++)
                    {
                        if (array3[num16, num17])
                        {
                            sketch.AddThing(ThingDefOf.Wall,
                                new IntVec3(num16 + custom3 + 3, 0, num17 + num5 + num4 + 2), Rot4.North, stuff);
                        }
                        else
                        {
                            sketch.AddTerrain(TerrainDefOf.Concrete,
                                new IntVec3(num16 + custom3 + 3, 0, num17 + num5 + num4 + 2));
                        }
                    }
                }

                sketch.AddThing(ThingDefOf.Door, new IntVec3(custom3 + 3, 0, num5 + num4 + 4), Rot4.East,
                    ThingDefOf.Steel);
                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.Battery, new IntVec3(custom3 + 4, 0, num5 + num4 + 3), Rot4.East);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.Battery, new IntVec3(custom3 + 4, 0, num5 + num4 + 5), Rot4.East);
                }
            }
        }

        var cr = default(CellRect);
        CellRect cellRect2;
        var cr2 = default(CellRect);
        if (num < num2)
        {
            if (generate)
            {
                cr = new CellRect(custom3 - num3, num5, num3, num4);
            }

            cellRect2 = new CellRect(custom3 + 3, num6 + 11, 6, 13);
            if (generate)
            {
                cr2 = new CellRect(custom3 - 4, num5 + num4 + 2, 4, 5);
            }
        }
        else
        {
            if (generate)
            {
                cr = new CellRect(custom3 + 3, num5, num3, num4);
            }

            cellRect2 = new CellRect(custom3 - 6, num6 + 11, 6, 13);
            if (generate)
            {
                cr2 = new CellRect(custom3 + 3, num5 + num4 + 2, 4, 5);
            }
        }

        var custom7 = parms.GetCustom<Rot4>("alleywayRotation");
        if (custom7 == Rot4.West)
        {
            if (generate)
            {
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37), 3);
            }

            cellRect2 = Utils.Rotate(cellRect2, new IntVec3(37, 0, 37), 3);
            if (generate)
            {
                cr2 = Utils.Rotate(cr2, new IntVec3(37, 0, 37), 3);
            }
        }
        else if (custom7 == Rot4.South)
        {
            if (generate)
            {
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37), 2);
            }

            cellRect2 = Utils.Rotate(cellRect2, new IntVec3(37, 0, 37), 2);
            if (generate)
            {
                cr2 = Utils.Rotate(cr2, new IntVec3(37, 0, 37), 2);
            }
        }
        else if (custom7 == Rot4.East)
        {
            if (generate)
            {
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37));
            }

            cellRect2 = Utils.Rotate(cellRect2, new IntVec3(37, 0, 37));
            if (generate)
            {
                cr2 = Utils.Rotate(cr2, new IntVec3(37, 0, 37));
            }
        }

        if (generate)
        {
            foreach (var cell in cr.Cells)
            {
                if (cell.x + custom6.x >= 0 && cell.x + custom6.x < custom5.Size.x && cell.z + custom6.z >= 0 &&
                    cell.z + custom6.z < custom5.Size.z)
                {
                    custom5.roofGrid.SetRoof(new IntVec3(cell.x + custom6.x, 0, cell.z + custom6.z),
                        RoofDefOf.RoofConstructed);
                }
            }
        }

        foreach (var cell2 in cellRect2.Cells)
        {
            if (cell2.x + custom6.x >= 0 && cell2.x + custom6.x < custom5.Size.x && cell2.z + custom6.z >= 0 &&
                cell2.z + custom6.z < custom5.Size.z)
            {
                custom5.roofGrid.SetRoof(new IntVec3(cell2.x + custom6.x, 0, cell2.z + custom6.z),
                    RoofDefOf.RoofConstructed);
            }
        }

        if (generate)
        {
            foreach (var cell3 in cr2.Cells)
            {
                if (cell3.x + custom6.x >= 0 && cell3.x + custom6.x < custom5.Size.x && cell3.z + custom6.z >= 0 &&
                    cell3.z + custom6.z < custom5.Size.z)
                {
                    custom5.roofGrid.SetRoof(new IntVec3(cell3.x + custom6.x, 0, cell3.z + custom6.z),
                        RoofDefOf.RoofConstructed);
                }
            }
        }

        parms.sketch.MergeAt(sketch, new IntVec3(0, 0, 0));
        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
    }
}