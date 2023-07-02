using RimWorld;
using Verse;

namespace TROM;

internal class CompEmptyStateRespawn : ThingComp
{
    private CompProperties_EmptyStateRespawn Props => (CompProperties_EmptyStateRespawn)props;

    public bool ParentIsEmpty
    {
        get
        {
            if (parent is Building_Casket { HasAnyContents: false })
            {
                return true;
            }

            var compPawnSpawnOnWakeup = parent.TryGetComp<CompPawnSpawnOnWakeup>();
            return compPawnSpawnOnWakeup is { CanSpawn: false };
        }
    }

    public override void CompTick()
    {
        base.CompTick();
        if (!ParentIsEmpty)
        {
            return;
        }

        var map = parent.Map;
        var building = GetBuilding(parent);
        building.HitPoints = parent.HitPoints;
        parent.Destroy();
        GenSpawn.SpawnBuildingAsPossible(building, map);
        var filth = GetFilth(parent);
        if (filth != null)
        {
            GenSpawn.Spawn(filth, building.Position, map);
        }
    }

    private Building GetBuilding(ThingWithComps thing)
    {
        if (thing.def == TROMThingDefOf.CrateATM)
        {
            var building = (Building)ThingMaker.MakeThing(TROMThingDefOf.AncientATM);
            building.Rotation = thing.Rotation;
            building.Position = thing.Position;
            return building;
        }

        if (thing.def == TROMThingDefOf.CrateBarrel)
        {
            var building2 = (Building)ThingMaker.MakeThing(ThingDefOf.AncientBarrel);
            building2.Rotation = thing.Rotation;
            building2.Position = thing.Position;
            return building2;
        }

        if (thing.def == TROMThingDefOf.CrateFridge)
        {
            var building3 = (Building)ThingMaker.MakeThing(TROMThingDefOf.AncientRefrigerator);
            building3.Rotation = thing.Rotation;
            building3.Position = thing.Position;
            return building3;
        }

        if (thing.def == TROMThingDefOf.CrateVendingMachine)
        {
            var building4 = (Building)ThingMaker.MakeThing(TROMThingDefOf.AncientVendingMachine);
            building4.Rotation = thing.Rotation;
            building4.Position = thing.Position;
            return building4;
        }

        if (thing.def != TROMThingDefOf.CrateWashingMachine)
        {
            return null;
        }

        var building5 = (Building)ThingMaker.MakeThing(TROMThingDefOf.AncientWashingMachine);
        building5.Rotation = thing.Rotation;
        building5.Position = thing.Position;
        return building5;
    }

    private ThingDef GetFilth(ThingWithComps thing)
    {
        return thing.def == TROMThingDefOf.CrateBarrel ? ThingDefOf.Filth_Fuel : null;
    }
}