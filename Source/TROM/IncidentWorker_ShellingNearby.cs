using RimWorld;
using UnityEngine;
using Verse;

namespace TROM;

public class IncidentWorker_ShellingNearby : IncidentWorker
{
    private const int FixedPoints = 30;

    protected override bool CanFireNowSub(IncidentParms parms)
    {
        var map = (Map)parms.target;
        if (!base.CanFireNowSub(parms))
        {
            return false;
        }

        return map.Biome == TROMBiomeDefOf.CityCentral || map.Biome == TROMBiomeDefOf.CityOutskirts;
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        var duration = Mathf.RoundToInt(def.durationDays.RandomInRange * 60000f);
        var gameCondition_ShellingNearby =
            (GameCondition_ShellingNearby)GameConditionMaker.MakeCondition(TROMDef.GameCondition_ShellingNearby,
                duration);
        map.gameConditionManager.RegisterCondition(gameCondition_ShellingNearby);
        SendStandardLetter(def.letterLabel, TROMDef.GameCondition_ShellingNearby.letterText, def.letterDef, parms,
            new TargetInfo(gameCondition_ShellingNearby.centerLocation.ToIntVec3, map));
        return true;
    }
}