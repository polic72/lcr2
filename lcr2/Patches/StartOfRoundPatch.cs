using HarmonyLib;


namespace lcr2.Patches;

[HarmonyPatch(typeof(StartOfRound))]
public class StartOfRoundPatch
{
    [HarmonyPatch("EndOfGame")]
    [HarmonyPrefix]
    private static void EndOfGame(StartOfRound __instance)
    {
        if (__instance.NetworkManager.IsServer)
        {
            HUDManager.Instance.AddTextToChatOnServer(RandomManager.log_text);
        }
    }
}
