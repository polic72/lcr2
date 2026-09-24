using System;
using System.Text;
using HarmonyLib;

namespace lcr2.Patches;

[HarmonyPatch(typeof(RoundManager))]
public class RandomManager
{
    protected static readonly Random Random = new Random();

    public static SelectableLevel? RealLevel;
    public static SelectableLevel? ScrapLevel;
    public static SelectableLevel? IndoorEnemiesLevel;
    public static SelectableLevel? OutdoorEnemiesLevel;
    public static string? log_text;

    [HarmonyPatch("LoadNewLevel")]
    [HarmonyPrefix]
    private static void LoadNewLevel(RoundManager __instance)
    {
        //TODO Remember to put this back to the regular RollLevels.
        log_text = RollLevels(__instance);
        //RollLevels_Debug(__instance);
        if (RealLevel == null) return;
        if (ScrapLevel == null) return;
        if (IndoorEnemiesLevel == null) return;
        if (OutdoorEnemiesLevel == null) return;

        //TODO Comment these out, otherwise players can cheat!
        lcr2.Logger.LogInfo("The actual level is \"" + RealLevel.PlanetName + "\"");
        lcr2.Logger.LogInfo("The scrap level is \"" + ScrapLevel.PlanetName + "\"");
        lcr2.Logger.LogInfo("The indoor level is \"" + IndoorEnemiesLevel.PlanetName + "\"");
        lcr2.Logger.LogInfo("The OutdoorEnemiesLevel level is \"" + OutdoorEnemiesLevel.PlanetName + "\"");
    }


    public static string RollLevels(RoundManager roundManager)
    {
        StringBuilder builder = new StringBuilder(256);

        RealLevel = roundManager.currentLevel;
        builder.Append($"Map: {RealLevel.PlanetName}\n");

        do
        {
            ScrapLevel =  roundManager.playersManager.levels[Random.Next(0, roundManager.playersManager.levels.Length)];
        } while (RandomManager.ScrapLevel.PlanetName == "71 Gordion");
        builder.Append($"Scrap: {ScrapLevel.PlanetName}\n");

        IndoorEnemiesLevel =  roundManager.playersManager.levels[Random.Next(0, roundManager.playersManager.levels.Length)];
        builder.Append($"Indoor: {IndoorEnemiesLevel.PlanetName}\n");

        do
        {
            OutdoorEnemiesLevel =  roundManager.playersManager.levels[Random.Next(0, roundManager.playersManager.levels.Length)];
            lcr2.Logger.LogInfo("Outdoor level (now " + OutdoorEnemiesLevel.PlanetName + ") was liquidation before");
        } while (OutdoorEnemiesLevel.PlanetName == "44 Liquidation");
        builder.Append($"Outdoor: {OutdoorEnemiesLevel.PlanetName}\n");

        return builder.ToString();
    }


    public static void RollLevels_Debug(RoundManager roundManager)
    {
        RealLevel = roundManager.currentLevel;

        SelectableLevel? dine = null;
        for (int i = 0; i < roundManager.playersManager.levels.Length; ++i)
        {
            SelectableLevel level = roundManager.playersManager.levels[i];
            string name = level.PlanetName.ToLower();

            if (name.StartsWith("7") && name.Contains("dine"))
            {
                dine = level;
                continue;
            }
        }

        ScrapLevel = dine ?? RealLevel;
        IndoorEnemiesLevel = dine ?? RealLevel;
        OutdoorEnemiesLevel = dine ?? RealLevel;
    }
}
