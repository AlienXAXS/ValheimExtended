using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using HarmonyLib;

namespace Mod.Harmony.Map
{
    [HarmonyPatch(typeof(Minimap), "OnTogglePublicPosition")]
    public static class OnTogglePublicPositionOverride
    {
        private static void PreFix(Minimap __instance)
        {
            var isReferencePositionPublic = ZNet.instance.IsReferencePositionPublic();

            var logLine = $"Toggle of isReferencePositionPublic detected, saving value of {isReferencePositionPublic}";
            Util.Logger.Instance.Log(logLine);
            Console.instance.Print(logLine);
            Settings.Instance.UpdateSetting(Settings.SettingTypes.REMEMBER_MAP_SHARING_MODE, isReferencePositionPublic);
        }
    }
}
