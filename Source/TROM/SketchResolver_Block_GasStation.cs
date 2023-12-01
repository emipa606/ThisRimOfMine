using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_Block_GasStation : SketchResolver
{
    private IntVec3 b;

    private CellRect CRNE;

    private CellRect CRNW;

    private CellRect CRSE;

    private CellRect crTmp;

    private CellRect GS;

    private Map map;

    private List<CellRect> RLT = [];

    private List<CellRect> roofList = [];

    private List<CellRect> roofListTmp = [];

    private Rot4 rotB;

    private variant var;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        roofListTmp = [];
        roofList = [];
        var = Rand.ElementByWeight(variant.normal, 0.7f, variant.squatters, 0.3f);
        map = parms.GetCustom<Map>("map");
        b = parms.GetCustom<IntVec3>("b");
        rotB = parms.GetCustom<Rot4>("rotationBlock");
        ResolveGasStation(parms);
        ResolveCorners(parms);
        ResolveEdges(parms);
        ResolveRoof();
        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms);
    }

    private void ResolveGasStation(ResolveParams parms)
    {
        parms.rect = new CellRect(0, 0, 19, 22);
        var resolveParams = parms;
        var sketch = resolveParams.sketch;
        if (resolveParams.rect != null)
        {
            GS = resolveParams.rect.Value;
        }

        var stuff = Filters.RandStone();
        var array = BoxShapeGenerator.Generate(8, 9);
        array[3, 0] = false;
        array[4, 8] = false;
        array[0, 4] = false;
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (array[i, j])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(i + 11, 0, j + 6), Rot4.North, stuff);
                }
            }
        }

        sketch.AddThing(ThingDefOf.Wall, new IntVec3(13, 0, 5), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(13, 0, 4), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(13, 0, 3), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(13, 0, 2), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(14, 0, 2), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(15, 0, 2), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(16, 0, 2), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(17, 0, 2), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 5), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 4), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 3), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 2), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(14, 0, 15), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(14, 0, 16), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(14, 0, 17), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(14, 0, 18), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(14, 0, 19), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(15, 0, 19), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(16, 0, 19), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(17, 0, 19), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(16, 0, 18), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(16, 0, 17), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 15), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 16), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 17), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 18), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Wall, new IntVec3(18, 0, 19), Rot4.North, stuff);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(2, 0, 7), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(2, 0, 13), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(6, 0, 7), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(6, 0, 13), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(10, 0, 7), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Column, new IntVec3(10, 0, 13), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(TROMThingDefOf.Autodoor, new IntVec3(11, 0, 10), Rot4.East, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(14, 0, 6), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Door, new IntVec3(15, 0, 14), Rot4.North);
        sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(15, 0, 17), Rot4.North);
        sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(17, 0, 17), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientFuelNode, new IntVec3(6, 0, 8), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientFuelNode, new IntVec3(6, 0, 9), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientFuelNode, new IntVec3(6, 0, 10), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientFuelNode, new IntVec3(6, 0, 11), Rot4.North);
        sketch.AddThing(ThingDefOf.AncientFuelNode, new IntVec3(6, 0, 12), Rot4.North);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 14), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 15), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 16), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 17), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 18), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 19), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 6), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 5), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 4), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 3), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(10, 0, 2), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 6), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 5), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 4), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 3), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 2), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 8), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 9), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 10), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 11), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 12), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 14), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 15), Rot4.North, ThingDefOf.Steel);
        sketch.AddThing(ThingDefOf.Fence, new IntVec3(2, 0, 16), Rot4.North, ThingDefOf.Steel);
        for (var k = 0; k < 9; k++)
        {
            for (var l = 0; l < 16; l++)
            {
                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(k + 2, 0, l));
            }
        }

        for (var m = 0; m < 11; m++)
        {
            for (var n = 0; n < 6; n++)
            {
                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(m, 0, n + 16));
            }
        }

        for (var num = 0; num < 8; num++)
        {
            for (var num2 = 0; num2 < 9; num2++)
            {
                sketch.AddTerrain(TerrainDefOf.PavedTile, new IntVec3(num + 11, 0, num2 + 6));
            }
        }

        for (var num3 = 0; num3 < 3; num3++)
        {
            for (var num4 = 0; num4 < 4; num4++)
            {
                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(num3 + 15, 0, num4 + 15));
            }
        }

        for (var num5 = 0; num5 < 4; num5++)
        {
            for (var num6 = 0; num6 < 3; num6++)
            {
                sketch.AddTerrain(TerrainDefOf.Concrete, new IntVec3(num5 + 14, 0, num6 + 3));
            }
        }

        sketch.AddThing(TROMThingDefOf.AncientToilet, new IntVec3(15, 0, 18), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientToilet, new IntVec3(17, 0, 18), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(16, 0, 15), Rot4.South);
        sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(17, 0, 15), Rot4.South);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(2, 0, 0), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(5, 0, 0), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(7, 0, 0), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(10, 0, 0), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(0, 0, 3), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(0, 0, 8), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(0, 0, 13), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(0, 0, 17), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(0, 0, 20), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(14, 0, 21), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(6, 0, 3), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(6, 0, 5), Rot4.North);
        sketch.AddThing(TROMThingDefOf.AncientConcreteBarrier, new IntVec3(6, 0, 15), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(7, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(8, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(9, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(10, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(11, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(12, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(13, 0, 21), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 0), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 1), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 2), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 4), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 5), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 6), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 7), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 9), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 10), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 11), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 12), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 14), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 15), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 16), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(13, 0, 1), Rot4.East);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(1, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(6, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(11, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(12, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(13, 0, 0), Rot4.North);
        sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(0, 0, 21), Rot4.North);
        if (var == variant.normal)
        {
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(17, 0, 5), Rot4.South);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(16, 0, 3), Rot4.North);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 3), Rot4.North);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(12, 0, 12), Rot4.East);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 13), Rot4.South);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 10), Rot4.West);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(15, 0, 11), Rot4.East);
            sketch.AddThing(ThingDefOf.Barricade, new IntVec3(12, 0, 8), Rot4.North);
            sketch.AddThing(ThingDefOf.Barricade, new IntVec3(13, 0, 8), Rot4.North);
            sketch.AddThing(ThingDefOf.Barricade, new IntVec3(14, 0, 8), Rot4.North);
            sketch.AddThing(ThingDefOf.Barricade, new IntVec3(15, 0, 8), Rot4.North);
        }
        else
        {
            sketch.AddThing(ThingDefOf.Bedroll, new IntVec3(14, 0, 3), Rot4.North);
            sketch.AddThing(ThingDefOf.Bedroll, new IntVec3(17, 0, 5), Rot4.West);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(12, 0, 11), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(13, 0, 11), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(14, 0, 11), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(14, 0, 10), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(14, 0, 9), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(14, 0, 8), Rot4.North);
            sketch.AddThing(ThingDefOf.Sandbags, new IntVec3(13, 0, 8), Rot4.North);
            sketch.AddThing(ThingDefOf.AncientSecurityTurret, new IntVec3(15, 0, 11), Rot4.West);
            sketch.AddThing(TROMThingDefOf.Shelf, new IntVec3(14, 0, 13), Rot4.South);
        }

        for (var num7 = 0; num7 < 5; num7++)
        {
            var @bool = Rand.Bool;
            var stackCount = Rand.Range(1, 5);
            if (@bool)
            {
                sketch.AddThing(TROMThingDefOf.AncientRefrigerator, new IntVec3(17, 0, num7 + 9), Rot4.East);
                continue;
            }

            var building_Crate = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateFridge);
            var def = Loot.Generate(Loot.lootType.Fridge);
            var thing = ThingMaker.MakeThing(def);
            thing.stackCount = stackCount;
            building_Crate.TryAcceptThing(thing);
            var iv = new IntVec3(17, 0, num7 + 9);
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
        }

        for (var num8 = 0; num8 < 3; num8++)
        {
            for (var num9 = 0; num9 < 4; num9++)
            {
                if (!Rand.Bool)
                {
                    continue;
                }

                var stackCount2 = Rand.Range(10, 20);
                if (Rand.Bool)
                {
                    sketch.AddThing(ThingDefOf.AncientBarrel, new IntVec3(11 + num8, 0, 15 + num9), Rot4.North);
                    sketch.AddThing(ThingDefOf.Filth_Fuel,
                        new IntVec3(Rand.Element(11 + num8 + 1, 11 + num8 - 1), 0,
                            Rand.Element(15 + num9 + 1, 15 + num9 - 1)), Rot4.Random);
                    continue;
                }

                var building_Crate2 = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateBarrel);
                var def2 = Loot.Generate(Loot.lootType.Barrel);
                var thing2 = ThingMaker.MakeThing(def2);
                thing2.stackCount = stackCount2;
                building_Crate2.TryAcceptThing(thing2);
                building_Crate2.Rotation = Rot4.North;
                var iv2 = new IntVec3(11 + num8, 0, 15 + num9);
                if (rotB == Rot4.North)
                {
                    building_Crate2.Position = new IntVec3(iv2.x + b.x, 0, iv2.z + b.z);
                }
                else if (rotB == Rot4.East)
                {
                    var intVec4 = Utils.Rotate(iv2, new IntVec3(37, 0, 37));
                    building_Crate2.Position = new IntVec3(intVec4.x + b.x, 0, intVec4.z + b.z);
                }
                else if (rotB == Rot4.South)
                {
                    var intVec5 = Utils.Rotate(iv2, new IntVec3(37, 0, 37), 2);
                    building_Crate2.Position = new IntVec3(intVec5.x + b.x, 0, intVec5.z + b.z);
                }
                else
                {
                    var intVec6 = Utils.Rotate(iv2, new IntVec3(37, 0, 37), 3);
                    building_Crate2.Position = new IntVec3(intVec6.x + b.x, 0, intVec6.z + b.z);
                }

                GenSpawn.SpawnBuildingAsPossible(building_Crate2, map);
            }
        }

        var stackCount3 = Rand.Range(10, 20);
        var building_Crate3 = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateATM);
        var def3 = Loot.Generate(Loot.lootType.ATM);
        var thing3 = ThingMaker.MakeThing(def3);
        thing3.stackCount = stackCount3;
        building_Crate3.TryAcceptThing(thing3);
        var iv3 = new IntVec3(10, 0, 8);
        if (rotB == Rot4.North)
        {
            building_Crate3.Rotation = Rot4.East;
            building_Crate3.Position = new IntVec3(iv3.x + b.x, 0, iv3.z + b.z);
        }
        else if (rotB == Rot4.East)
        {
            building_Crate3.Rotation = Rot4.South;
            var intVec7 = Utils.Rotate(iv3, new IntVec3(37, 0, 37));
            building_Crate3.Position = new IntVec3(intVec7.x + b.x, 0, intVec7.z + b.z);
        }
        else if (rotB == Rot4.South)
        {
            building_Crate3.Rotation = Rot4.West;
            var intVec8 = Utils.Rotate(iv3, new IntVec3(37, 0, 37), 2);
            building_Crate3.Position = new IntVec3(intVec8.x + b.x, 0, intVec8.z + b.z);
        }
        else
        {
            building_Crate3.Rotation = Rot4.North;
            var intVec9 = Utils.Rotate(iv3, new IntVec3(37, 0, 37), 3);
            building_Crate3.Position = new IntVec3(intVec9.x + b.x, 0, intVec9.z + b.z);
        }

        GenSpawn.SpawnBuildingAsPossible(building_Crate3, map);
        for (var num10 = 0; num10 < 2; num10++)
        {
            var stackCount4 = Rand.Range(2, 5);
            if (Rand.Range(0, 100) < 70)
            {
                sketch.AddThing(TROMThingDefOf.AncientVendingMachine, new IntVec3(10, 0, 11 + num10), Rot4.East);
                continue;
            }

            var building_Crate4 = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateVendingMachine);
            var def4 = Loot.Generate(Loot.lootType.VendingMachine);
            var thing4 = ThingMaker.MakeThing(def4);
            thing4.stackCount = stackCount4;
            building_Crate4.TryAcceptThing(thing4);
            var iv4 = new IntVec3(10, 0, 11 + num10);
            if (rotB == Rot4.North)
            {
                building_Crate4.Position = new IntVec3(iv4.x + b.x, 0, iv4.z + b.z);
                building_Crate4.Rotation = Rot4.East;
            }
            else if (rotB == Rot4.East)
            {
                var intVec10 = Utils.Rotate(iv4, new IntVec3(37, 0, 37));
                building_Crate4.Position = new IntVec3(intVec10.x + b.x, 0, intVec10.z + b.z);
                building_Crate4.Rotation = Rot4.South;
            }
            else if (rotB == Rot4.South)
            {
                var intVec11 = Utils.Rotate(iv4, new IntVec3(37, 0, 37), 2);
                building_Crate4.Position = new IntVec3(intVec11.x + b.x, 0, intVec11.z + b.z);
                building_Crate4.Rotation = Rot4.West;
            }
            else
            {
                var intVec12 = Utils.Rotate(iv4, new IntVec3(37, 0, 37), 3);
                building_Crate4.Position = new IntVec3(intVec12.x + b.x, 0, intVec12.z + b.z);
                building_Crate4.Rotation = Rot4.North;
            }

            GenSpawn.SpawnBuildingAsPossible(building_Crate4, map);
        }

        parms.sketch.MergeAt(resolveParams.sketch, new IntVec3(0, 0, 0));
    }

    private void ResolveCorners(ResolveParams parms)
    {
        var parms2 = parms;
        parms2.sketch = new Sketch();
        parms2.rect = new CellRect(0, 0, 0, 0);
        var num = Rand.Range(0, 100);
        if (num <= 20)
        {
            RLT = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms2);
        }
        else if (num <= 50)
        {
            RLT = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms2);
        }
        else
        {
            RLT = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms2);
        }

        parms2.sketch.Rotate(Rot4.West);
        CRSE = new CellRect(38 - parms2.sketch.OccupiedRect.Width, 0, parms2.sketch.OccupiedRect.Width,
            parms2.sketch.OccupiedRect.Height);
        foreach (var item in RLT)
        {
            crTmp = Utils.Rotate(item, new IntVec3(37, 0, 37), 3);
            roofListTmp.Add(new CellRect(crTmp.minX, crTmp.minZ, crTmp.Width, crTmp.Height));
        }

        parms.sketch.MergeAt(parms2.sketch,
            new IntVec3(CRSE.minX + parms2.sketch.OccupiedRect.Width - 1, 0, CRSE.minZ));
        var parms3 = parms;
        parms3.sketch = new Sketch();
        parms3.rect = new CellRect(0, 0, 0, 0);
        var num2 = Rand.Range(0, 100);
        if (num2 <= 20)
        {
            RLT = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms3);
        }
        else if (num2 <= 50)
        {
            RLT = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms3);
        }
        else
        {
            RLT = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms3);
        }

        parms3.sketch.Rotate(Rot4.East);
        CRNW = new CellRect(0, 38 - parms3.sketch.OccupiedRect.Height, parms3.sketch.OccupiedRect.Width,
            parms3.sketch.OccupiedRect.Height);
        foreach (var item2 in RLT)
        {
            crTmp = Utils.Rotate(item2, new IntVec3(37, 0, 37));
            roofListTmp.Add(crTmp);
        }

        parms.sketch.MergeAt(parms3.sketch,
            new IntVec3(CRNW.minX, 0, CRNW.minZ + parms3.sketch.OccupiedRect.Height - 1));
        var parms4 = parms;
        parms4.sketch = new Sketch();
        parms4.rect = new CellRect(0, 0, 0, 0);
        var num3 = Rand.Range(0, 100);
        if (num3 <= 20)
        {
            RLT = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms4);
        }
        else if (num3 <= 50)
        {
            RLT = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms4);
        }
        else
        {
            RLT = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms4);
        }

        parms4.sketch.Rotate(Rot4.South);
        CRNE = new CellRect(38 - parms4.sketch.OccupiedRect.Width, 38 - parms4.sketch.OccupiedRect.Height,
            parms4.sketch.OccupiedRect.Width, parms4.sketch.OccupiedRect.Height);
        foreach (var item3 in RLT)
        {
            crTmp = Utils.Rotate(item3, new IntVec3(37, 0, 37), 2);
            roofListTmp.Add(crTmp);
        }

        parms.sketch.MergeAt(parms4.sketch,
            new IntVec3(CRNE.minX + parms4.sketch.OccupiedRect.Width - 1, 0,
                CRNE.minZ + parms4.sketch.OccupiedRect.Height - 1));
    }

    private void ResolveEdges(ResolveParams parms)
    {
        int num;
        int num2;
        var i = GS.Width;
        for (var num3 = 38 - CRSE.Width; num3 - i > 0; i += num)
        {
            var resolveParams = parms;
            resolveParams.sketch = new Sketch();
            num2 = Rand.Range(7, 10);
            resolveParams.SetCustom("backhouse", Rand.Bool);
            resolveParams.SetCustom("min", i);
            resolveParams.SetCustom("rotation", Rot4.North);
            if (num3 - i <= 11)
            {
                num = num3 - i;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num3 - i - num < 5)
                {
                    num = num3 - i - 5;
                }
            }

            var sketchResolverDef = ChooseHouseEdgeType(num);
            resolveParams.rect = new CellRect(0, 0, num, num2);
            sketchResolverDef.Resolve(resolveParams);
            parms.sketch.MergeAt(resolveParams.sketch, new IntVec3(i, 0, 0));
            if (sketchResolverDef == TROMSketchResolverDefOf.HouseEdge)
            {
                roofListTmp.Add(new CellRect(i, 0, num, num2));
                continue;
            }

            foreach (var roof in GetRoofList(sketchResolverDef, resolveParams))
            {
                roofListTmp.Add(new CellRect(i + roof.minX, roof.minZ, roof.Width, roof.Height));
            }
        }

        var j = CRNW.Height;
        for (var num4 = 38 - GS.Height; num4 - j > 0; j += num)
        {
            var resolveParams2 = parms;
            resolveParams2.sketch = new Sketch();
            num2 = Rand.Range(7, 10);
            resolveParams2.SetCustom("backhouse", Rand.Bool);
            resolveParams2.SetCustom("min", j);
            resolveParams2.SetCustom("rotation", Rot4.East);
            if (num4 - j <= 11)
            {
                num = num4 - j;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num4 - j - num < 5)
                {
                    num = num4 - j - 5;
                }
            }

            var sketchResolverDef2 = ChooseHouseEdgeType(num);
            resolveParams2.rect = new CellRect(0, 0, num, num2);
            sketchResolverDef2.Resolve(resolveParams2);
            resolveParams2.sketch.Rotate(Rot4.East);
            parms.sketch.MergeAt(resolveParams2.sketch, new IntVec3(0, 0, 37 - j));
            if (sketchResolverDef2 == TROMSketchResolverDefOf.HouseEdge)
            {
                var cr = new CellRect(j, 0, num, num2);
                roofListTmp.Add(Utils.Rotate(cr, new IntVec3(37, 0, 37)));
                continue;
            }

            foreach (var roof2 in GetRoofList(sketchResolverDef2, resolveParams2))
            {
                var cr2 = new CellRect(j + roof2.minX, roof2.minZ, roof2.Width, roof2.Height);
                roofListTmp.Add(Utils.Rotate(cr2, new IntVec3(37, 0, 37)));
            }
        }

        var k = CRNE.Width;
        for (var num5 = 38 - CRNW.Width; num5 - k > 0; k += num)
        {
            var resolveParams3 = parms;
            resolveParams3.sketch = new Sketch();
            num2 = Rand.Range(7, 10);
            resolveParams3.SetCustom("backhouse", Rand.Bool);
            resolveParams3.SetCustom("min", k);
            resolveParams3.SetCustom("rotation", Rot4.South);
            if (num5 - k <= 11)
            {
                num = num5 - k;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num5 - k - num < 5)
                {
                    num = num5 - k - 5;
                }
            }

            var sketchResolverDef3 = ChooseHouseEdgeType(num);
            resolveParams3.rect = new CellRect(0, 0, num, num2);
            sketchResolverDef3.Resolve(resolveParams3);
            resolveParams3.sketch.Rotate(Rot4.South);
            parms.sketch.MergeAt(resolveParams3.sketch, new IntVec3(37 - k, 0, 37));
            if (sketchResolverDef3 == TROMSketchResolverDefOf.HouseEdge)
            {
                var cr3 = new CellRect(k, 0, num, num2);
                roofListTmp.Add(Utils.Rotate(cr3, new IntVec3(37, 0, 37), 2));
                continue;
            }

            foreach (var roof3 in GetRoofList(sketchResolverDef3, resolveParams3))
            {
                var cr4 = new CellRect(k + roof3.minX, roof3.minZ, roof3.Width, roof3.Height);
                roofListTmp.Add(Utils.Rotate(cr4, new IntVec3(37, 0, 37), 2));
            }
        }

        var l = CRSE.Height;
        for (var num6 = 38 - CRNE.Height; num6 - l > 0; l += num)
        {
            var resolveParams4 = parms;
            resolveParams4.sketch = new Sketch();
            num2 = Rand.Range(7, 10);
            resolveParams4.SetCustom("backhouse", Rand.Bool);
            resolveParams4.SetCustom("min", l);
            resolveParams4.SetCustom("rotation", Rot4.West);
            if (num6 - l <= 11)
            {
                num = num6 - l;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num6 - l - num < 5)
                {
                    num = num6 - l - 5;
                }
            }

            var sketchResolverDef4 = ChooseHouseEdgeType(num);
            resolveParams4.rect = new CellRect(0, 0, num, num2);
            sketchResolverDef4.Resolve(resolveParams4);
            resolveParams4.sketch.Rotate(Rot4.West);
            parms.sketch.MergeAt(resolveParams4.sketch, new IntVec3(37, 0, l));
            if (sketchResolverDef4 == TROMSketchResolverDefOf.HouseEdge)
            {
                var cr5 = new CellRect(l, 0, num, num2);
                roofListTmp.Add(Utils.Rotate(cr5, new IntVec3(37, 0, 37), 3));
                continue;
            }

            foreach (var roof4 in GetRoofList(sketchResolverDef4, resolveParams4))
            {
                var cr6 = new CellRect(l + roof4.minX, roof4.minZ, roof4.Width, roof4.Height);
                roofListTmp.Add(Utils.Rotate(cr6, new IntVec3(37, 0, 37), 3));
            }
        }
    }

    private void ResolveRoof()
    {
        roofListTmp.Add(new CellRect(2, 7, 9, 7));
        roofListTmp.Add(new CellRect(11, 6, 8, 9));
        roofListTmp.Add(new CellRect(13, 2, 6, 4));
        roofListTmp.Add(new CellRect(14, 15, 5, 5));
        foreach (var item in roofListTmp)
        {
            if (rotB == Rot4.West)
            {
                roofList.Add(Utils.Rotate(item, new IntVec3(37, 0, 37), 3));
            }
            else if (rotB == Rot4.South)
            {
                roofList.Add(Utils.Rotate(item, new IntVec3(37, 0, 37), 2));
            }
            else if (rotB == Rot4.East)
            {
                roofList.Add(Utils.Rotate(item, new IntVec3(37, 0, 37)));
            }
            else
            {
                roofList.Add(item);
            }
        }

        foreach (var roof in roofList)
        {
            foreach (var cell in roof.Cells)
            {
                map.roofGrid.SetRoof(new IntVec3(b.x + cell.x, 0, b.z + cell.z), RoofDefOf.RoofConstructed);
            }
        }
    }

    private SketchResolverDef ChooseHouseEdgeType(int width)
    {
        return width switch
        {
            5 => Rand.Element(TROMSketchResolverDefOf.HouseEdge, TROMSketchResolverDefOf.HouseEdge_R5_Laundrette),
            7 => Rand.Element(TROMSketchResolverDefOf.HouseEdge, TROMSketchResolverDefOf.HouseEdge_R7_Parking),
            11 => Rand.Element(TROMSketchResolverDefOf.HouseEdge, TROMSketchResolverDefOf.HouseEdge_R11_1),
            _ => TROMSketchResolverDefOf.HouseEdge
        };
    }

    private List<CellRect> GetRoofList(SketchResolverDef sr, ResolveParams rp = default)
    {
        if (sr == TROMSketchResolverDefOf.HouseEdge_R5_Laundrette)
        {
            return SketchResolver_HouseEdge_R5_Laundrette.roofList;
        }

        if (sr != TROMSketchResolverDefOf.HouseEdge_R11_1)
        {
            return [];
        }

        var list = new List<CellRect> { new CellRect(0, 0, 11, 7) };
        if (!rp.GetCustom<bool>("backhouseAllow"))
        {
            return list;
        }

        list.Add(rp.GetCustom<bool>("backhouseVariant") ? new CellRect(2, 7, 9, 4) : new CellRect(0, 7, 9, 4));

        return list;
    }

    private enum variant
    {
        normal,
        squatters
    }
}