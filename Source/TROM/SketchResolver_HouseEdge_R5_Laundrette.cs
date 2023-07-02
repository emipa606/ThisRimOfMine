using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_HouseEdge_R5_Laundrette : SketchResolver
{
    public static readonly int x = 5;

    public static readonly int z = 9;

    public static readonly List<CellRect> roofList = new List<CellRect>
    {
        new CellRect(0, 0, 5, 9)
    };

    private IntVec3 b;

    private Map map;

    private int min;

    private Rot4 rot;

    private Rot4 rotB;

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        parms.rect = new CellRect(parms.rect.Value.minX, parms.rect.Value.minZ, x, z);
        map = parms.GetCustom<Map>("map");
        b = parms.GetCustom<IntVec3>("b");
        rot = parms.GetCustom<Rot4>("rotation");
        rotB = parms.GetCustom<Rot4>("rotationBlock");
        min = parms.GetCustom<int>("min");
        var thingDef = Filters.RandStone();
        var num = x;
        var num2 = 9;
        var sketch = new Sketch();
        var array = BoxShapeGenerator.Generate(num, num2);
        array[2, 0] = false;
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
            parms3.rect = new CellRect(1, 1, num - 2, num2 - 2);
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
            parms3.rect = new CellRect(2, 0, 1, 1);
            parms3.thingCentral = thingDef;
            TROMSketchResolverDefOf.FloorFillSpecific.Resolve(parms3);
        }

        var things = sketch.Things;
        for (var k = 0; k < things.Count; k++)
        {
            if (things[k].def == ThingDefOf.Wall)
            {
                sketch.RemoveTerrain(things[k].pos);
            }
        }

        sketch.AddThing(ThingDefOf.Door, new IntVec3(2, 0, 0), Rot4.North);
        if (Rand.Bool)
        {
            sketch.AddThing(ThingDefOf.Stool, new IntVec3(3, 0, 1), Rot4.East);
        }

        if (Rand.Bool)
        {
            sketch.AddThing(ThingDefOf.Stool, new IntVec3(3, 0, 2), Rot4.East);
        }

        if (Rand.Bool)
        {
            sketch.AddThing(ThingDefOf.Stool, new IntVec3(3, 0, 3), Rot4.East);
        }

        if (Rand.Bool)
        {
            sketch.AddThing(ThingDefOf.Stool, new IntVec3(3, 0, 4), Rot4.East);
        }

        if (Rand.Bool)
        {
            sketch.AddThing(ThingDefOf.Stool, new IntVec3(3, 0, 5), Rot4.East);
        }

        if (Rand.Bool)
        {
            sketch.AddThing(TROMThingDefOf.AncientKitchenSink, new IntVec3(3, 0, 7), Rot4.North);
        }

        //Log.Warning(rotB.ToString());
        for (var l = 0; l < 5; l++)
        {
            if (Rand.Bool)
            {
                sketch.AddThing(TROMThingDefOf.AncientWashingMachine, new IntVec3(1, 0, l + 1), Rot4.West);
                continue;
            }

            var building_Crate = (Building_Crate)ThingMaker.MakeThing(TROMThingDefOf.CrateWashingMachine);
            var def = Loot.Generate(Loot.lootType.WashingMachine);
            var thing = ThingMaker.MakeThing(def, ThingDefOf.Cloth);
            thing.HitPoints = (int)(thing.MaxHitPoints * Rand.Range(0.5f, 0.75f));
            building_Crate.TryAcceptThing(thing);
            var iv = new IntVec3(min + 1, 0, l + 1);
            var relativeRotation = Utils.RelativeRotation(rotB, rot);
            if (relativeRotation == Rot4.North)
            {
                building_Crate.Rotation = Rot4.West;
                building_Crate.Position = new IntVec3(iv.x + b.x, 0, iv.z + b.z);
            }
            else if (relativeRotation == Rot4.East)
            {
                building_Crate.Rotation = Rot4.North;
                var intVec = Utils.Rotate(iv, new IntVec3(37, 0, 37));
                building_Crate.Position = new IntVec3(intVec.x + b.x, 0, intVec.z + b.z);
            }
            else if (relativeRotation == Rot4.South)
            {
                building_Crate.Rotation = Rot4.East;
                var intVec2 = Utils.Rotate(iv, new IntVec3(37, 0, 37), 2);
                building_Crate.Position = new IntVec3(intVec2.x + b.x, 0, intVec2.z + b.z);
            }
            else
            {
                building_Crate.Rotation = Rot4.South;
                var intVec3 = Utils.Rotate(iv, new IntVec3(37, 0, 37), 3);
                building_Crate.Position = new IntVec3(intVec3.x + b.x, 0, intVec3.z + b.z);
            }

            GenSpawn.SpawnBuildingAsPossible(building_Crate, map);
        }

        TROMSketchResolverDefOf.PlaceDamage.Resolve(parms2);
        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}