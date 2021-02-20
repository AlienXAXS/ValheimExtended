using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace Mod.Harmony.UI.MainMenu.FejdStartup
{
    [HarmonyPatch(typeof(global::FejdStartup), "SetupGui")]
    public static class FejdStartupOverrides
    {
        private static void Postfix(global::FejdStartup __instance)
        {
            global::FejdStartup.instance.m_versionLabel.resizeTextForBestFit = true;
            global::FejdStartup.instance.m_versionLabel.text = $"Valheim v{Version.GetVersionString()}\n<color=yellow>AlienX'ss Mod v{Utilities.ModVersionHandler.GetVersion()}</color>";
        }
    }
}