using HarmonyLib;
using HomelyUnderWhereCompat;
using RimWorld;
using System.Collections.Generic;
using Verse;

[HarmonyPatch(typeof(PillowTalkWithEuterpe.HomelyUtilities), "GetApparelToUndress")]
public static class GetApparelToUndressPatch
{
    public static void Postfix(Pawn pawn, List<Apparel> __result)
    {
        bool hasTop = pawn.apparel.WornApparel.Any(a =>
            a.def.apparel.layers.Any(l =>
                l.defName == "UnderwearTop"));

        bool hasBottom = pawn.apparel.WornApparel.Any(a =>
            a.def.apparel.layers.Any(l =>
                l.defName == "Underwear"));

        if (hasTop)
        {
            AddTorsoClothing(pawn, __result);
        }

        if (hasBottom)
        {
            AddLegClothing(pawn, __result);
        }
    }

    private static void AddTorsoClothing(Pawn pawn, List<Apparel> result)
    {
        foreach (Apparel apparel in pawn.apparel.WornApparel)
        {
            if (result.Contains(apparel))
                continue;

            if (!apparel.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.Torso))
                continue;

            if (IsUnderwear(apparel))
                continue;

            if (apparel.def.apparel.layers.Contains(ApparelLayerDefOf.OnSkin))
            {
                result.Add(apparel);

                DebugLog.Message(
                    $"[HomelyUnderWhereCompat] Added torso clothing: {apparel.def.label}"
                );
            }
        }
    }

    private static void AddLegClothing(Pawn pawn, List<Apparel> result)
    {
        foreach (Apparel apparel in pawn.apparel.WornApparel)
        {
            if (result.Contains(apparel))
                continue;

            if (!apparel.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.Legs))
                continue;

            if (IsUnderwear(apparel))
                continue;

            if (apparel.def.apparel.layers.Contains(ApparelLayerDefOf.OnSkin))
            {
                result.Add(apparel);

                DebugLog.Message(
                     $"[HomelyUnderWhereCompat] Added leg clothing: {apparel.def.label}"
                );
            }
        }
    }

    private static bool IsUnderwear(Apparel apparel)
    {
        return apparel.def.apparel.layers.Any(l =>
            l.defName == "Underwear" ||
            l.defName == "UnderwearTop");
    }
}