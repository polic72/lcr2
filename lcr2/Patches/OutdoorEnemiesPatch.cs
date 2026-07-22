using HarmonyLib;


namespace lcr2.Patches;


[HarmonyPatch(typeof(RoundManager))]
public class OutdoorEnemiesPatch
{
    [HarmonyPatch(typeof (RoundManager), "PredictAllOutsideEnemies")]
    [HarmonyPrefix]
    public static void PredictAllOutsideEnemies(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.OutdoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "SpawnEnemiesOutside")]
    [HarmonyPrefix]
    public static void SpawnEnemiesOutside(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.OutdoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "SpawnRandomOutsideEnemy")]
    [HarmonyPrefix]
    public static void SpawnRandomOutsideEnemy(RoundManager __instance)
    {
        __instance.currentLevel = RandomManager.OutdoorEnemiesLevel;
    }

    [HarmonyPatch(typeof (RoundManager), "SpawnRandomOutsideEnemy")]
    [HarmonyPostfix]
    public static void SpawnRandomOutsideEnemy_Debug(RoundManager __instance)
    {
        EnemyAI spawned_enemy = __instance.SpawnedEnemies[__instance.SpawnedEnemies.Count - 1];

        lcr2.Logger.LogInfo("Spawned " + spawned_enemy.enemyType.name + " outside");
    }

    [HarmonyPatch(typeof (RoundManager), "RefreshEnemiesList")]
    [HarmonyPrefix]
    public static void RefreshEnemiesList(RoundManager __instance)
    {
        __instance.currentLevel.maxOutsideEnemyPowerCount = RandomManager.OutdoorEnemiesLevel?.maxOutsideEnemyPowerCount ?? 0;
    }
}
