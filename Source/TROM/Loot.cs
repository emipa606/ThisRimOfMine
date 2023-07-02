using RimWorld;
using Verse;

namespace TROM;

internal static class Loot
{
    public enum lootType
    {
        ATM,
        Barrel,
        FoodGreens,
        FoodMeat,
        FoodMeals,
        Fridge,
        Clothing,
        Tech,
        VendingMachine,
        WashingMachine
    }

    public static ThingDef Generate(lootType lt)
    {
        return lt switch
        {
            lootType.ATM => ThingDefOf.Silver,
            lootType.Barrel => ThingDefOf.Chemfuel,
            lootType.FoodGreens => ThingCategoryDefOf.PlantFoodRaw.childThingDefs.RandomElement(),
            lootType.FoodMeat => ThingCategoryDefOf.MeatRaw.childThingDefs.RandomElement(),
            lootType.Fridge => Rand.Element(ThingDefOf.Beer, ThingDefOf.MealSurvivalPack, ThingDefOf.MealSimple),
            lootType.VendingMachine => ThingDefOf.Chocolate,
            lootType.WashingMachine => Rand.Element(TROMThingDefOf.Apparel_BasicShirt,
                TROMThingDefOf.Apparel_CollarShirt, TROMThingDefOf.Apparel_Duster, TROMThingDefOf.Apparel_Pants,
                TROMThingDefOf.Apparel_Parka),
            _ => ThingDefOf.WoodLog
        };
    }
}