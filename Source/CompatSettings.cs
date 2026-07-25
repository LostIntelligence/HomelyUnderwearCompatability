using Verse;

namespace HomelyUnderWhereCompat
{
    public class CompatSettings : ModSettings
    {
        public bool debugLogging = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(
                ref debugLogging,
                "debugLogging",
                false
            );

            base.ExposeData();
        }
    }
}