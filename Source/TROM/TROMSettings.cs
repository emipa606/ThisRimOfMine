using Verse;

namespace TROM;

public class TROMSettings : ModSettings
{
    public bool cityRestriction = true;
    public bool lightStreets = true;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref lightStreets, "lightStreets", true);
        Scribe_Values.Look(ref cityRestriction, "cityRestriction", true);
        base.ExposeData();
    }
}