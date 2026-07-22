using HarmonyLib;


namespace lcr2.Patches;

[HarmonyPatch(typeof(RoundManager))]
public class ScrapPatch
{
    [HarmonyPatch(typeof(RoundManager), "SpawnScrapInLevel")]
    [HarmonyPrefix]
    public static void SpawnScrapInLevel(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.ScrapLevel;
    }


    [HarmonyPatch(typeof(RoundManager), "SpawnMapObjects")]
    [HarmonyPrefix]
    public static void SpawnMapObjects(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.ScrapLevel;
    }
}
