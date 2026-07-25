using UnityEngine;
using Verse;

namespace HomelyUnderWhereCompat
{
    public class CompatMod : Mod
    {
        public static CompatSettings Settings;

        public CompatMod(ModContentPack content)
            : base(content)
        {
            Settings = GetSettings<CompatSettings>();
        }

        public override string SettingsCategory()
        {
            return "Homely × UnderWhere Compatibility";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();

            listing.Begin(inRect);

            listing.CheckboxLabeled(
                "Enable debug logging",
                ref Settings.debugLogging
            );

            listing.Gap();

            listing.Label(
                "Debug logging prints compatibility decisions to the RimWorld log."
            );

            listing.End();
        }
    }
}