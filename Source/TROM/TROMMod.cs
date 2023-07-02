using Mlie;
using UnityEngine;
using Verse;

namespace TROM;

public class TROMMod : Mod
{
    private static string currentVersion;
    private readonly TROMSettings settings;

    public TROMMod(ModContentPack content)
        : base(content)
    {
        settings = GetSettings<TROMSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);
        listing_Standard.CheckboxLabeled("TROM.light".Translate(), ref settings.lightStreets, ".");
        listing_Standard.CheckboxLabeled("TROM.biomes".Translate(), ref settings.cityRestriction, ".");
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("TROM.currentversion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "This Rim Of Mine";
    }
}