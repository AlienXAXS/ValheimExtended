using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace Mod.Harmony.Game
{
    [HarmonyPatch(typeof(global::Game), "FindSpawnPoint")]
    public static class GameOverride
    {
        private static void Postfix(global::Game __instance)
        {
            if (global::Game.instance != null)
            {
                if (global::Game.instance.WaitingForRespawn())
                {
                    if (!global::Game.instance.GetPlayerProfile().HaveLogoutPoint()) return;

                    var logLine = $"Player respawn detected, setting options.";
                    Util.Logger.Instance.Log(logLine);
                    Console.instance.Print(logLine);

                    Util.Logger.Instance.Log($"Setting SetPublicReferencePosition = {Settings.Instance.Container.RememberMapSharingMode}");
                    ZNet.instance.SetPublicReferencePosition(Settings.Instance.Container.RememberMapSharingMode);
                }
            }
        }
    }
}
