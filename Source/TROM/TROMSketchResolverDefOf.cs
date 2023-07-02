using RimWorld;

namespace TROM;

[DefOf]
public static class TROMSketchResolverDefOf
{
    public static SketchResolverDef Block_GasStation;

    public static SketchResolverDef Block_Residential;

    public static SketchResolverDef Block_Superstore;

    public static SketchResolverDef Courtyard;

    public static SketchResolverDef CourtyardGarage;

    public static SketchResolverDef CourtyardGarden;

    public static SketchResolverDef CourtyardShed;

    public static SketchResolverDef FloorFillSpecific;

    public static SketchResolverDef HouseAlleyway;

    public static SketchResolverDef HouseCorner_R1;

    public static SketchResolverDef HouseCorner_R2;

    public static SketchResolverDef HouseCorner_RG;

    public static SketchResolverDef HouseEdge;

    public static SketchResolverDef HouseEdge_R5_Laundrette;

    public static SketchResolverDef HouseEdge_R7_Parking;

    public static SketchResolverDef HouseEdge_R11_1;

    public static SketchResolverDef Intersection;

    public static SketchResolverDef Intersection_Artillery;

    public static SketchResolverDef Intersection_BarbedWire;

    public static SketchResolverDef Intersection_Checkpoint;

    public static SketchResolverDef PlaceDamage;

    public static SketchResolverDef PlaceFilth;

    public static SketchResolverDef PlaceLoot;

    public static SketchResolverDef Street;

    public static SketchResolverDef Street_Barricade;

    static TROMSketchResolverDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(SketchResolverDefOf));
    }
}