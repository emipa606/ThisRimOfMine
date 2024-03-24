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
        new Harmony("TROM").PatchAll(Assembly.GetExecutingAssembly());
    }
}