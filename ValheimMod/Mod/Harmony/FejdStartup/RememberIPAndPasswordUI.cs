using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Mod.Harmony.FejdStartup
{
    [HarmonyPatch(typeof(global::FejdStartup), "OnJoinIPOpen")]
    public class RememberIPAndPasswordUI
    {
        private static void Prefix()
        {
            global::FejdStartup.instance.m_joinIPAddress.text = Settings.Instance.Container.LastIPAddress;
            global::FejdStartup.instance.m_joinIPAddress.ActivateInputField();
        }
    }

    [HarmonyPatch(typeof(global::FejdStartup), "OnJoinIPConnect")]
    public class OnJoinIPConnectPatch
    {
        private static void Prefix(global::FejdStartup __instance)
        {
            Settings.Instance.UpdateSetting(Settings.SettingTypes.LAST_IP_ADDRESS, __instance.m_joinIPAddress.text);
        }
    }
}
