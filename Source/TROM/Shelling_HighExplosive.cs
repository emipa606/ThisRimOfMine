using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace TROM;

[StaticConstructorOnStartup]
public class Shelling_HighExplosive : Bombardment
{
    public new const int EffectiveAreaRadius = 23;

    private const int StartRandomFireEveryTicks = 20;

    private const int EffectDuration = 60;

    private static readonly Material ProjectileMaterial =
        MaterialPool.MatFrom("Things/Projectile/Bullet_Big", ShaderDatabase.Transparent, Color.white);

    public new static readonly SimpleCurve DistanceChanceFactor =
    [
        new CurvePoint(0f, 1f),
        new CurvePoint(1f, 0.1f)
    ];

    public new int bombIntervalTicks = 18;

    private bool done;

    public new int explosionCount = 20;

    public new FloatRange explosionRadiusRange = new FloatRange(3f, 6f);
    public new float impactAreaRadius = 15f;

    private IntVec3 nextExplosionCell = IntVec3.Invalid;

    private BombardmentProjectile projectile = new BombardmentProjectile();

    private List<BombardmentProjectile> projectiles = [];

    public new int randomFireRadius = 25;

    private int ticksToNextEffect;

    public new int warmupTicks = 60;

    protected new int TicksLeft => duration - TicksPassed;

    public override void SpawnSetup(Map map, bool respawningAfterReload)
    {
        base.explosionCount = 1;
        base.SpawnSetup(map, respawningAfterReload);
        if (!respawningAfterReload)
        {
            GetNextExplosionCell();
        }
    }

    public override void Tick()
    {
        if (Destroyed)
        {
            return;
        }

        if (warmupTicks > 0)
        {
            warmupTicks--;
            if (warmupTicks == 0)
            {
                StartStrike();
            }
        }
        else
        {
            if (done)
            {
                base.Tick();
            }

            if (Find.TickManager.TicksGame % 20 == 0 && TicksLeft > 0)
            {
                StartRandomFire();
            }
        }

        EffectTick();
    }

    private void EffectTick()
    {
        if (!nextExplosionCell.IsValid)
        {
            ticksToNextEffect = warmupTicks - bombIntervalTicks;
            GetNextExplosionCell();
        }

        ticksToNextEffect--;
        if (ticksToNextEffect <= 0 && TicksLeft >= bombIntervalTicks && !done)
        {
            SoundDefOf.Bombardment_PreImpact.PlayOneShot(new TargetInfo(nextExplosionCell, Map));
            projectiles.Add(new BombardmentProjectile(60, nextExplosionCell));
            ticksToNextEffect = bombIntervalTicks;
            GetNextExplosionCell();
            done = true;
        }

        for (var num = projectiles.Count - 1; num >= 0; num--)
        {
            projectiles[num].Tick();
            if (projectiles[num].LifeTime > 0)
            {
                continue;
            }

            TryDoExplosion(projectiles[num]);
            projectiles.RemoveAt(num);
        }
    }

    private void TryDoExplosion(BombardmentProjectile proj)
    {
        var list = Map.listerThings.ThingsInGroup(ThingRequestGroup.Facility);
        foreach (var thingFound in list)
        {
            if (thingFound.TryGetComp<CompProjectileInterceptor>().CheckBombardmentIntercept(this, proj))
            {
                return;
            }
        }

        var randomInRange = explosionRadiusRange.RandomInRange;
        TryDestroyRoof(Map, proj.targetCell);
        var targetCell = proj.targetCell;
        var map = Map;
        var bomb = DamageDefOf.Bomb;
        var thing = instigator;
        var thingDef = def;
        GenExplosion.DoExplosion(targetCell, map, randomInRange, bomb, thing, -1, -1f, null, weaponDef, thingDef);
    }

    public override void Draw()
    {
        base.Draw();
        if (projectiles.NullOrEmpty())
        {
            return;
        }

        foreach (var bombardmentProjectile in projectiles)
        {
            bombardmentProjectile.Draw(ProjectileMaterial);
        }
    }

    private void StartRandomFire()
    {
        var intVec = (from x in GenRadial.RadialCellsAround(Position, randomFireRadius, true)
            where x.InBounds(Map)
            select x).RandomElementByWeight(x => DistanceChanceFactor.Evaluate(x.DistanceTo(Position)));
        var list = Map.listerThings.ThingsInGroup(ThingRequestGroup.Facility);
        foreach (var thing in list)
        {
            if (!thing.TryGetComp<CompProjectileInterceptor>().BombardmentCanStartFireAt(this, intVec))
            {
                return;
            }
        }

        FireUtility.TryStartFireIn(intVec, Map, Rand.Range(0.1f, 0.925f));
    }

    private void GetNextExplosionCell()
    {
        nextExplosionCell = (from x in GenRadial.RadialCellsAround(Position, impactAreaRadius, true)
            where x.InBounds(Map)
            select x).RandomElementByWeight(x =>
            DistanceChanceFactor.Evaluate(x.DistanceTo(Position) / impactAreaRadius));
    }

    private void TryDestroyRoof(Map map, IntVec3 pos)
    {
        for (var i = -1; i < 2; i++)
        {
            for (var j = -1; j < 2; j++)
            {
                RoofCollapserImmediate.DropRoofInCells(new IntVec3(pos.x + i, 0, pos.z + j), map);
                if (map.thingGrid.CellContains(new IntVec3(pos.x + i, 0, pos.z + j), ThingDefOf.Wall) && Rand.Bool)
                {
                    GenSpawn.Spawn(ThingDefOf.CollapsedRocks, new IntVec3(pos.x + i, 0, pos.z + j), map);
                }
            }
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref impactAreaRadius, "impactAreaRadius", 15f);
        Scribe_Values.Look(ref explosionRadiusRange, "explosionRadiusRange", new FloatRange(6f, 8f));
        Scribe_Values.Look(ref randomFireRadius, "randomFireRadius", 25);
        Scribe_Values.Look(ref bombIntervalTicks, "bombIntervalTicks", 18);
        Scribe_Values.Look(ref warmupTicks, "warmupTicks");
        Scribe_Values.Look(ref ticksToNextEffect, "ticksToNextEffect");
        Scribe_Values.Look(ref nextExplosionCell, "nextExplosionCell");
        Scribe_Collections.Look(ref projectiles, "projectiles", LookMode.Deep);
        if (Scribe.mode != LoadSaveMode.PostLoadInit)
        {
            return;
        }

        if (!nextExplosionCell.IsValid)
        {
            GetNextExplosionCell();
        }

        if (projectiles == null)
        {
            projectiles = [];
        }
    }
}