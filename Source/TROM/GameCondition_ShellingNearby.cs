using RimWorld;
using Verse;
using Verse.Sound;

namespace TROM;

public class GameCondition_ShellingNearby : GameCondition
{
    private static readonly IntRange TicksBetweenStrikes = new IntRange(300, 900);

    private static readonly IntRange TicksBetweenAmbience = new IntRange(120, 600);

    public bool ambientSound;

    public IntVec2 centerLocation = IntVec2.Invalid;

    public IntRange initialStrikeDelay = new IntRange(300, 600);

    public int nextAmbientTicks = 30;

    private int nextStrikeTicks;

    private Sustainer soundSustainer;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref centerLocation, "centerLocation");
        Scribe_Values.Look(ref nextStrikeTicks, "nextStrikeTicks");
        Scribe_Values.Look(ref initialStrikeDelay, "initialStrikeDelay");
        Scribe_Values.Look(ref ambientSound, "ambientSound");
    }

    public override void Init()
    {
        base.Init();
        nextStrikeTicks = Find.TickManager.TicksGame + initialStrikeDelay.RandomInRange;
        nextAmbientTicks = Find.TickManager.TicksGame + TicksBetweenAmbience.RandomInRange;
        if (centerLocation.IsInvalid)
        {
            FindGoodCenterLocation();
        }
    }

    public override void GameConditionTick()
    {
        if (Find.TickManager.TicksGame > nextStrikeTicks)
        {
            GenSpawn.Spawn(TROMThingDefOf.Shelling_HighExplosive, centerLocation.ToIntVec3, SingleMap);
            nextStrikeTicks = Find.TickManager.TicksGame + TicksBetweenStrikes.RandomInRange;
            FindGoodCenterLocation();
        }

        if (Find.TickManager.TicksGame <= nextAmbientTicks)
        {
            return;
        }

        TROMDef.Shelling_OffMap.PlayOneShot(new TargetInfo(Find.CameraDriver.MapPosition, SingleMap));
        nextAmbientTicks = Find.TickManager.TicksGame + TicksBetweenAmbience.RandomInRange;
    }

    private void FindGoodCenterLocation()
    {
        if (SingleMap.Size.x <= 16 || SingleMap.Size.z <= 16)
        {
            Log.Error("Map too small for shelling.");
        }

        for (var i = 0; i < 10; i++)
        {
            centerLocation = new IntVec2(Rand.Range(8, SingleMap.Size.x - 8), Rand.Range(8, SingleMap.Size.z - 8));
        }
    }
}