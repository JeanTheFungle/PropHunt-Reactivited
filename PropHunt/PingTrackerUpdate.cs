using HarmonyLib;
using UnityEngine;

namespace PropHunt
{
    //[HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {

        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();

            __instance.text.text =
                "<color=#00FF00FF>PropHunt v" + PropHuntPlugin.VersionString + "</color>\n" +
                $"Ping: {AmongUsClient.Instance.Ping}ms\n" +
                (!MeetingHud.Instance
                    ? "<color=#00FF00FF>Modded By: ugackMiner53 &</color>\n" +
                    "<color=#00FF00FF>Continued : JeanAU</color>\n" : "") +
                (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started
                    ? "<color=#00FF00FF>Formerly:Continued by : JeanAU</color>" : "");
        }
    }
}