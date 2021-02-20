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
            Utilities.GameVersionHandler.Instance.GameVersion = __instance.m_versionLabel.text;
            global::FejdStartup.instance.m_versionLabel.resizeTextForBestFit = true;
            global::FejdStartup.instance.m_versionLabel.text = $"Valheim v{Utilities.GameVersionHandler.Instance.GameVersion}\n<color=yellow>AlienX's Mod v{Utilities.ModVersionHandler.GetVersion()}</color>";
        }
    }
}
