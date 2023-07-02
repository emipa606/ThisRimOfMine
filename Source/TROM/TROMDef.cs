using RimWorld;
using Verse;

namespace TROM;

[DefOf]
internal class TROMDef
{
    public static GameConditionDef GameCondition_ShellingNearby;

    public static GenStepDef TROM_ElevationFertility;

    public static GenStepDef TROM_Terrain;

    public static GenStepDef TROM_Streets;

    public static GenStepDef TROM_Blocks;

    public static IncidentDef ShellingNearby;

    public static MapGeneratorDef TROMBase_Player;

    public static SoundDef Shelling_OffMap;

    public static WorldGenStepDef TROM_City;

    static TROMDef()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(TROMDef));
    }
}