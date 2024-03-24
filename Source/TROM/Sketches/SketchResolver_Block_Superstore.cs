using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_Block_Superstore : SketchResolver
{
    private IntVec3 b;

    private Map map;
    private Rot4 rotB;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        var sketch = new Sketch();
        rotB = parms.GetCustom<Rot4>("rotationBlock");
        map = parms.GetCustom<Map>("map");
        b = parms.GetCustom<IntVec3>("b");
        var array = new bool[38, 38];
        GenerateArray(array);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(20, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(20, 0, 24), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(25, 0, 26), Rot4.North, ThingDefOf.Steel);
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(i, 0, j), Rot4.North, TROMThingDefOf.BlocksLimestone);
                }

                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(i, 0, j));
            }
        }

        var fenceDef = ThingDef.Named("Fence");
        var plantPotDef = ThingDef.Named("PlantPot");
        var stoolDef = ThingDef.Named("Stool");
        sketch.AddThing(ThingDefOf.Door, new IntVec3(0, 0, 29), Rot4.East, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(3, 0, 28), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(4, 0, 28), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(30, 0, 37), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.Autodoor, new IntVec3(29, 0, 13), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.Autodoor, new IntVec3(30, 0, 13), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.Autodoor, new IntVec3(32, 0, 15), Rot4.East, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.Autodoor, new IntVec3(32, 0, 16), Rot4.East, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(32, 0, 29), Rot4.East, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(34, 0, 33), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(35, 0, 25), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(8, 0, 19), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(8, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.AnimalFlap, new IntVec3(1, 0, 18), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AnimalFlap, new IntVec3(5, 0, 18), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AnimalFlap, new IntVec3(6, 0, 18), Rot4.North);
        sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(9, 0, 14), Rot4.East, ThingDefOf.WoodLog);
        sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(9, 0, 16), Rot4.East, ThingDefOf.WoodLog);
        sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(29, 0, 27), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 14), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 15), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 18), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 19), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 20), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 24), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 25), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 26), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(26, 0, 27), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(27, 0, 27), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(28, 0, 27), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(30, 0, 27), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(fenceDef, new IntVec3(31, 0, 27), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.AncientToilet, new IntVec3(10, 0, 14), Rot4.East);
        sketch.AddThing(TROMThingDefOf.AncientToilet, new IntVec3(10, 0, 16), Rot4.East);
        sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(10, 0, 18), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(9, 0, 22), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(9, 0, 25), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(10, 0, 24), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(10, 0, 27), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(1, 0, 32), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(1, 0, 34), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(4, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(6, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(8, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(10, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(3, 0, 31), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(3, 0, 33), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(4, 0, 32), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(4, 0, 34), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(6, 0, 31), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(6, 0, 33), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(7, 0, 32), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(7, 0, 34), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(9, 0, 31), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(9, 0, 33), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(10, 0, 32), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(10, 0, 34), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(16, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(18, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(20, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(23, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(25, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(27, 0, 36), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(29, 0, 36), Rot4.South);
        for (var k = 0; k < 6; k++)
        {
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(13 + (k * 3), 0, 31), Rot4.West);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(13 + (k * 3), 0, 33), Rot4.West);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14 + (k * 3), 0, 32), Rot4.East);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14 + (k * 3), 0, 34), Rot4.East);
        }

        sketch.AddThing(ThingDefOf.Table2x2c, new IntVec3(14, 0, 27), Rot4.North);
        sketch.AddThing(ThingDefOf.Table2x2c, new IntVec3(16, 0, 27), Rot4.North);
        sketch.AddThing(ThingDefOf.Table2x2c, new IntVec3(14, 0, 16), Rot4.North);
        sketch.AddThing(ThingDefOf.Table2x2c, new IntVec3(16, 0, 16), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(12, 0, 16), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(12, 0, 18), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(13, 0, 14), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(15, 0, 14), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(17, 0, 14), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 20), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 24), Rot4.West);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(15, 0, 21), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(15, 0, 25), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(16, 0, 20), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(16, 0, 24), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(17, 0, 21), Rot4.East);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(17, 0, 25), Rot4.East);
        sketch.AddThing(ThingDefOf.Table2x2c, new IntVec3(20, 0, 27), Rot4.North);
        sketch.AddThing(ThingDefOf.Table2x2c, new IntVec3(22, 0, 27), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(27, 0, 28), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(30, 0, 28), Rot4.North);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(22, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(23, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(24, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(25, 0, 21), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(22, 0, 24), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(23, 0, 24), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(24, 0, 24), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Barricade, new IntVec3(25, 0, 24), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Table1x2c, new IntVec3(33, 0, 34), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Armchair, new IntVec3(32, 0, 35), Rot4.East, ThingDefOf.Leather_Plain);
        sketch.AddThing(ThingDefOf.DiningChair, new IntVec3(35, 0, 35), Rot4.West);
        sketch.AddThing(plantPotDef, new IntVec3(36, 0, 36), Rot4.North);
        sketch.AddThing(plantPotDef, new IntVec3(33, 0, 26), Rot4.North);
        sketch.AddThing(ThingDefOf.Table1x2c, new IntVec3(35, 0, 29), Rot4.East);
        sketch.AddThing(stoolDef, new IntVec3(35, 0, 28), Rot4.North);
        sketch.AddThing(stoolDef, new IntVec3(36, 0, 28), Rot4.North);
        sketch.AddThing(stoolDef, new IntVec3(35, 0, 30), Rot4.North);
        sketch.AddThing(stoolDef, new IntVec3(36, 0, 30), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(34, 0, 24), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(34, 0, 21), Rot4.South);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(33, 0, 22), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(33, 0, 19), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(35, 0, 19), Rot4.North);
        sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(35, 0, 22), Rot4.East);
        for (var l = 0; l < 3; l++)
        {
            for (var m = 0; m < 4; m++)
            {
                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(3 + (12 * l), 0, 1 + (2 * m)), Rot4.West);
                }

                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(10 + (12 * l), 0, 2 + (2 * m)), Rot4.East);
                }
            }
        }

        sketch.AddThing(TROMThingDefOf.AncientRustedTruck, new IntVec3(4, 0, 13), Rot4.South);
        sketch.AddThing(TROMThingDefOf.AncientContainer, new IntVec3(3, 0, 18), Rot4.North);
        sketch.AddThing(ThingDefOf.Cooler, new IntVec3(7, 0, 26), Rot4.West);
        sketch.AddThing(ThingDefOf.Cooler, new IntVec3(7, 0, 25), Rot4.West);
        sketch.AddThing(ThingDefOf.AncientFence, new IntVec3(8, 0, 12), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientFence, new IntVec3(17, 0, 12), Rot4.North);
        for (var n = 0; n < 8; n++)
        {
            sketch.AddThing(TROMThingDefOf.AncientShoppingCart, new IntVec3(9 + n, 0, 12), Rot4.Random);
        }

        var stackCount = Rand.Range(10, 20);
        var building_Crate = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateATM);
        var def = Loot.Generate(Loot.lootType.ATM);
        var thing = ThingMaker.MakeThing(def);
        thing.stackCount = stackCount;
        building_Crate.TryAcceptThing(thing);
        var iv = new IntVec3(31, 0, 24);
        if (rotB == Rot4.North)
        {
            building_Crate.Rotation = Rot4.East;
            building_Crate.Position = new IntVec3(iv.x + b.x, 0, iv.z + b.z);
        }
        else if (rotB == Rot4.East)
        {
            building_Crate.Rotation = Rot4.South;
            var intVec = Utils.Rotate(iv, new IntVec3(37, 0, 37));
            building_Crate.Position = new IntVec3(intVec.x + b.x, 0, intVec.z + b.z);
        }
        else if (rotB == Rot4.South)
        {
            building_Crate.Rotation = Rot4.West;
            var intVec2 = Utils.Rotate(iv, new IntVec3(37, 0, 37), 2);
            building_Crate.Position = new IntVec3(intVec2.x + b.x, 0, intVec2.z + b.z);
        }
        else
        {
            building_Crate.Rotation = Rot4.North;
            var intVec3 = Utils.Rotate(iv, new IntVec3(37, 0, 37), 3);
            building_Crate.Position = new IntVec3(intVec3.x + b.x, 0, intVec3.z + b.z);
        }

        GenSpawn.SpawnBuildingAsPossible(building_Crate, map);
        for (var num = 0; num < 2; num++)
        {
            for (var num2 = 0; num2 < 6; num2++)
            {
                if (Rand.Bool)
                {
                    if (num == 0)
                    {
                        sketch.AddThing(TROMThingDefOf.AncientRefrigerator, new IntVec3(20 + num2, 0, 14), Rot4.South);
                    }
                    else
                    {
                        sketch.AddThing(TROMThingDefOf.AncientRefrigerator, new IntVec3(20 + num2, 0, 19), Rot4.North);
                    }

                    continue;
                }

                var building_Crate2 = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateFridge);
                IntVec3 intVec4;
                if (num == 0)
                {
                    var def2 = Loot.Generate(Loot.lootType.Fridge);
                    var thing2 = ThingMaker.MakeThing(def2);
                    thing2.stackCount = Rand.Range(1, 5);
                    building_Crate2.TryAcceptThing(thing2);
                    intVec4 = new IntVec3(20 + num2, 0, 14);
                    if (rotB == Rot4.North)
                    {
                        building_Crate2.Rotation = Rot4.South;
                        building_Crate2.Position = new IntVec3(intVec4.x + b.x, 0, intVec4.z + b.z);
                    }
                    else if (rotB == Rot4.East)
                    {
                        building_Crate2.Rotation = Rot4.West;
                        var intVec5 = Utils.Rotate(intVec4, new IntVec3(37, 0, 37));
                        building_Crate2.Position = new IntVec3(intVec5.x + b.x, 0, intVec5.z + b.z);
                    }
                    else if (rotB == Rot4.South)
                    {
                        building_Crate2.Rotation = Rot4.North;
                        var intVec6 = Utils.Rotate(intVec4, new IntVec3(37, 0, 37), 2);
                        building_Crate2.Position = new IntVec3(intVec6.x + b.x, 0, intVec6.z + b.z);
                    }
                    else
                    {
                        building_Crate2.Rotation = Rot4.East;
                        var intVec7 = Utils.Rotate(intVec4, new IntVec3(37, 0, 37), 3);
                        building_Crate2.Position = new IntVec3(intVec7.x + b.x, 0, intVec7.z + b.z);
                    }
                }
                else
                {
                    var def3 = Loot.Generate(Loot.lootType.FoodMeat);
                    var thing3 = ThingMaker.MakeThing(def3);
                    thing3.stackCount = Rand.Range(5, 10);
                    building_Crate2.TryAcceptThing(thing3);
                    intVec4 = new IntVec3(20 + num2, 0, 19);
                    if (rotB == Rot4.North)
                    {
                        building_Crate2.Rotation = Rot4.North;
                        building_Crate2.Position = new IntVec3(intVec4.x + b.x, 0, intVec4.z + b.z);
                    }
                    else if (rotB == Rot4.East)
                    {
                        building_Crate2.Rotation = Rot4.East;
                        var intVec8 = Utils.Rotate(intVec4, new IntVec3(37, 0, 37));
                        building_Crate2.Position = new IntVec3(intVec8.x + b.x, 0, intVec8.z + b.z);
                    }
                    else if (rotB == Rot4.South)
                    {
                        building_Crate2.Rotation = Rot4.South;
                        var intVec9 = Utils.Rotate(intVec4, new IntVec3(37, 0, 37), 2);
                        building_Crate2.Position = new IntVec3(intVec9.x + b.x, 0, intVec9.z + b.z);
                    }
                    else
                    {
                        building_Crate2.Rotation = Rot4.West;
                        var intVec10 = Utils.Rotate(intVec4, new IntVec3(37, 0, 37), 3);
                        building_Crate2.Position = new IntVec3(intVec10.x + b.x, 0, intVec10.z + b.z);
                    }
                }

                GenSpawn.SpawnBuildingAsPossible(building_Crate2, map);
            }
        }

        var building_Crate3 = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateStorage);
        var def4 = Loot.Generate(Loot.lootType.ATM);
        var thing4 = ThingMaker.MakeThing(def4);
        thing4.stackCount = Rand.Range(20, 100);
        building_Crate3.TryAcceptThing(thing4);
        var iv2 = new IntVec3(36, 0, 34);
        if (rotB == Rot4.North)
        {
            building_Crate3.Rotation = Rot4.South;
            building_Crate3.Position = new IntVec3(iv2.x + b.x, 0, iv2.z + b.z);
        }
        else if (rotB == Rot4.East)
        {
            building_Crate3.Rotation = Rot4.West;
            var intVec11 = Utils.Rotate(iv2, new IntVec3(37, 0, 37));
            building_Crate3.Position = new IntVec3(intVec11.x + b.x, 0, intVec11.z + b.z);
        }
        else if (rotB == Rot4.South)
        {
            building_Crate3.Rotation = Rot4.North;
            var intVec12 = Utils.Rotate(iv2, new IntVec3(37, 0, 37), 2);
            building_Crate3.Position = new IntVec3(intVec12.x + b.x, 0, intVec12.z + b.z);
        }
        else
        {
            building_Crate3.Rotation = Rot4.East;
            var intVec13 = Utils.Rotate(iv2, new IntVec3(37, 0, 37), 3);
            building_Crate3.Position = new IntVec3(intVec13.x + b.x, 0, intVec13.z + b.z);
        }

        GenSpawn.SpawnBuildingAsPossible(building_Crate3, map);
        for (var num3 = 0; num3 < 4; num3++)
        {
            if (Rand.Bool)
            {
                sketch.AddThing(TROMThingDefOf.AncientVendingMachine, new IntVec3(31, 0, 18 + num3), Rot4.East);
                continue;
            }

            var building_Crate4 = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateVendingMachine);
            var def5 = Loot.Generate(Loot.lootType.VendingMachine);
            var thing5 = ThingMaker.MakeThing(def5);
            thing5.stackCount = Rand.Range(2, 5);
            building_Crate4.TryAcceptThing(thing5);
            var iv3 = new IntVec3(31, 0, 18 + num3);
            if (rotB == Rot4.North)
            {
                building_Crate4.Position = new IntVec3(iv3.x + b.x, 0, iv3.z + b.z);
                building_Crate4.Rotation = Rot4.East;
            }
            else if (rotB == Rot4.East)
            {
                var intVec14 = Utils.Rotate(iv3, new IntVec3(37, 0, 37));
                building_Crate4.Position = new IntVec3(intVec14.x + b.x, 0, intVec14.z + b.z);
                building_Crate4.Rotation = Rot4.South;
            }
            else if (rotB == Rot4.South)
            {
                var intVec15 = Utils.Rotate(iv3, new IntVec3(37, 0, 37), 2);
                building_Crate4.Position = new IntVec3(intVec15.x + b.x, 0, intVec15.z + b.z);
                building_Crate4.Rotation = Rot4.West;
            }
            else
            {
                var intVec16 = Utils.Rotate(iv3, new IntVec3(37, 0, 37), 3);
                building_Crate4.Position = new IntVec3(intVec16.x + b.x, 0, intVec16.z + b.z);
                building_Crate4.Rotation = Rot4.North;
            }

            GenSpawn.SpawnBuildingAsPossible(building_Crate4, map);
        }

        var list = new List<CellRect>
        {
            new CellRect(0, 18, 38, 20),
            new CellRect(7, 13, 26, 5)
        };
        var list2 = new List<CellRect>();
        foreach (var item in list)
        {
            if (rotB == Rot4.West)
            {
                list2.Add(Utils.Rotate(item, new IntVec3(37, 0, 37), 3));
            }
            else if (rotB == Rot4.South)
            {
                list2.Add(Utils.Rotate(item, new IntVec3(37, 0, 37), 2));
            }
            else if (rotB == Rot4.East)
            {
                list2.Add(Utils.Rotate(item, new IntVec3(37, 0, 37)));
            }
            else
            {
                list2.Add(item);
            }
        }

        foreach (var item2 in list2)
        {
            foreach (var cell in item2.Cells)
            {
                map.roofGrid.SetRoof(new IntVec3(b.x + cell.x, 0, b.z + cell.z), RoofDefOf.RoofConstructed);
            }
        }

        parms.sketch.MergeAt(sketch, new IntVec3(0, 0, 0));
        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms);
        return;

        bool[,] GenerateArray(bool[,] a)
        {
            a = ShapeGenerator.xLine(a, new IntVec2(0, 37), 38);
            a = ShapeGenerator.zLine(a, new IntVec2(0, 18), 20);
            a = ShapeGenerator.zLine(a, new IntVec2(37, 18), 20);
            a = ShapeGenerator.xLine(a, new IntVec2(0, 28), 12);
            a = ShapeGenerator.zLine(a, new IntVec2(7, 13), 15);
            a = ShapeGenerator.xLine(a, new IntVec2(7, 13), 25);
            a = ShapeGenerator.zLine(a, new IntVec2(32, 14), 19);
            a = ShapeGenerator.xLine(a, new IntVec2(33, 18), 4);
            a = ShapeGenerator.xLine(a, new IntVec2(33, 25), 4);
            a = ShapeGenerator.zLine(a, new IntVec2(11, 14), 6);
            a = ShapeGenerator.zLine(a, new IntVec2(11, 21), 7);
            a = ShapeGenerator.zLine(a, new IntVec2(31, 33), 4);
            a = ShapeGenerator.xLine(a, new IntVec2(32, 33), 5);
            a[1, 23] = true;
            a[2, 23] = true;
            a[5, 23] = true;
            a[6, 23] = true;
            a[9, 21] = true;
            a[10, 21] = true;
            a[9, 19] = true;
            a[10, 19] = true;
            a[9, 17] = true;
            a[10, 17] = true;
            a[9, 15] = true;
            a[10, 15] = true;
            array[0, 29] = false;
            array[3, 28] = false;
            array[4, 28] = false;
            array[30, 37] = false;
            array[29, 13] = false;
            array[30, 13] = false;
            array[32, 15] = false;
            array[32, 16] = false;
            array[32, 29] = false;
            array[34, 33] = false;
            array[35, 25] = false;
            return a;
        }
    }
}