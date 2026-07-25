using Verse;

namespace HomelyUnderWhereCompat
{
    public static class DebugLog
    {
        public static void Message(string text)
        {
            if (CompatMod.Settings != null &&
                CompatMod.Settings.debugLogging)
            {
                Log.Message(
                    "[HomelyUnderWhereCompat] " + text
                );
            }
        }
    }
}