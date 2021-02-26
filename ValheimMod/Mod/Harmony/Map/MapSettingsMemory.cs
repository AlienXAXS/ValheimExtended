using HarmonyLib;
using Mod.Utilities;

namespace Mod.Harmony.Map
{
    [HarmonyPatch(typeof(Minimap), "OnTogglePublicPosition")]
    public static class OnTogglePublicPositionOverride
    {
        private static void Postfix(ref Minimap __instance)
        {
            var isReferencePositionPublic = __instance.m_publicPosition.isOn;

            var logLine = $"Toggle of isReferencePositionPublic detected, saving value of {isReferencePositionPublic}";
            Logger.Log(logLine);
            Console.instance.Print(logLine);
            Settings.Instance.UpdateSetting(Settings.SettingTypes.REMEMBER_MAP_SHARING_MODE, isReferencePositionPublic);
        }
    }
}
