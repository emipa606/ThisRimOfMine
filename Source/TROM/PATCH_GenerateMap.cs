using System;
using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace TROM;

[HarmonyPatch(typeof(MapGenerator), "GenerateMap")]
internal class PATCH_GenerateMap
{
    [HarmonyPrefix]
    private static bool TROM_GenerateMap(MapParent parent, ref MapGeneratorDef mapGenerator)
    {
        try
        {
            if (parent.Biome == TROMBiomeDefOf.CityOutskirts || parent.Biome == TROMBiomeDefOf.CityCentral)
            {
                mapGenerator = TROMDef.TROMBase_Player;
            }

            return true;
        }
        catch (Exception ex)
        {
            Log.Warning($"TROM: Something went wrong @PATCH_GenerateMap: {ex}");
            return true;
        }
    }
}