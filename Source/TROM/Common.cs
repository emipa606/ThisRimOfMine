using System.Collections.Generic;
using RimWorld;
using Verse;

namespace TROM;

[StaticConstructorOnStartup]
public static class Common
{
    public static readonly List<ThingDef> Furniture =
    [
        ThingDefOf.AncientBed,
        ThingDefOf.AncientLamp,
        ThingDefOf.DiningChair,
        ThingDef.Named("PlantPot"),
        ThingDefOf.Table1x2c,
        ThingDefOf.Table2x2c,
        ThingDef.Named("Table3x3c"),
        TROMThingDefOf.AncientKitchenSink,
        TROMThingDefOf.AncientOven,
        TROMThingDefOf.AncientRefrigerator,
        TROMThingDefOf.AncientStove,
        TROMThingDefOf.AncientToilet,
        TROMThingDefOf.AncientVendingMachine,
        TROMThingDefOf.AncientWashingMachine,
        TROMThingDefOf.Armchair,
        TROMThingDefOf.ChessTable,
        TROMThingDefOf.CrateATM,
        TROMThingDefOf.CrateBarrel,
        TROMThingDefOf.CrateFridge,
        TROMThingDefOf.CrateStorage,
        TROMThingDefOf.CrateVendingMachine,
        TROMThingDefOf.CrateWashingMachine,
        TROMThingDefOf.Shelf,
        TROMThingDefOf.TubeTelevision
    ];
}