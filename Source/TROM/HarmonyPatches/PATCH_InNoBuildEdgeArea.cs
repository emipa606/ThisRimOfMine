using System;
using HarmonyLib;
using Verse;

namespace TROM;

[HarmonyPatch(typeof(GenGrid), "InNoBuildEdgeArea")]
internal class PATCH_InNoBuildEdgeArea
{
    [HarmonyPostfix]
    private static void TROM_InNoBuildEdgeArea(Map map, ref bool __result)
    {
        try
        {
            if (Current.ProgramState == ProgramState.MapInitializing && (map.Biome == TROMBiomeDefOf.CityOutskirts ||
                                                                         map.Biome == TROMBiomeDefOf.CityCentral))
            {
                __result = false;
            }
        }
        catch (Exception ex)
        {
            Log.Warning($"TROM: Something went wrong @PATCH_InNoBuildEdgeArea: {ex}");
        }
    }
}