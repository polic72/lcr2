using HarmonyLib;


namespace lcr2.Patches;

[HarmonyPatch(typeof(RoundManager))]
public class IndoorEnemiesPatch
{
    [HarmonyPatch(typeof (RoundManager), "PlotOutEnemiesForNextHour")]
    [HarmonyPrefix]
    public static void PlotOutEnemiesForNextHour(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.IndoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "AssignRandomEnemyToVent")]
    [HarmonyPrefix]
    public static void AssignRandomEnemyToVent(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.IndoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "AssignRandomEnemyToVent")]
    [HarmonyPostfix]
    public static void AssignRandomEnemyToVent_PostDebug(RoundManager __instance, EnemyVent vent)
    {
        lcr2.Logger.LogInfo("Assigned a " + vent.enemyType.name + " to vent " + vent.name);
    }

    [HarmonyPatch(typeof (RoundManager), "InsideEnemyCannotBeSpawned")]
    [HarmonyPrefix]
    public static void EnemyCannotBeSpawned(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.IndoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "BeginEnemySpawning")]
    [HarmonyPrefix]
    public static void BeginEnemySpawning(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.IndoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "RefreshEnemiesList")]
    [HarmonyPrefix]
    public static void RefreshEnemyList(RoundManager __instance)
    {
        __instance.currentLevel.maxEnemyPowerCount = RandomManager.IndoorEnemiesLevel?.maxEnemyPowerCount ?? 0;
    }
}
