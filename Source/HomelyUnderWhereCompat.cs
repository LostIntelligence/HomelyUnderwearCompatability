using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HomelyUnderWhereCompat
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            Log.Message("[HomelyUnderWhereCompat] Initializing");

            Harmony harmony = new Harmony("lc.homelyunderwherecompat");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            DebugLog.Message("[HomelyUnderWhereCompat] Harmony patches applied");

            MarkUnderwearAsSleepwear();
        }

        private static void MarkUnderwearAsSleepwear()
        {
            foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefsListForReading)
            {
                if (thingDef.apparel == null || thingDef.apparel.layers == null)
                    continue;

                bool isUnderwear =
                    thingDef.apparel.layers.Exists(x => x.defName == "Underwear") ||
                    thingDef.apparel.layers.Exists(x => x.defName == "UnderwearTop");

                if (!isUnderwear)
                    continue;

                if (thingDef.GetModExtension<PillowTalkWithEuterpe.HomelyModExtension>() != null)
                    continue;

                if (thingDef.modExtensions == null)
                    thingDef.modExtensions = new List<DefModExtension>();

                thingDef.modExtensions.Add(
                    new PillowTalkWithEuterpe.HomelyModExtension
                    {
                        isSleepwear = true
                    }
                );

                Log.Message(
                    $"{thingDef.label} was marked as sleepwear by Homely × UnderWhere Compatibility."
                );
            }
        }
    }
}