using System.Reflection;
using HarmonyLib;
using Verse;

namespace TROM;

[StaticConstructorOnStartup]
public static class Startup
{
    static Startup()
    {
        Harmony.DEBUG = false;
        var harmony = new Harmony("TROM");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}