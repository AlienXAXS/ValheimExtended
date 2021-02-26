using HarmonyLib;
using Mod.Events;
using UnityEngine.UI;

namespace Mod.Harmony.Network
{
    [HarmonyPatch(typeof(ZNet), "Awake")]
    public class ZNetHook
    {
        private static void Postfix()
        {
            if (ZNet.m_isServer)
            {
                if (ZNet.m_openServer)
                {
                    EventRouter.Instance.GameEvents.ServerStartedInvoke();
                }
            }
        }
    }

    [HarmonyPatch(typeof(ZNet), "RPC_ClientHandshake")]
    public class ZNetPasswordHook
    {
        private static void Postfix(ZNet __instance, ZRpc rpc, bool needPassword)
        {
            if (needPassword)
            {
                InputField componentInChildren = __instance.m_passwordDialog.GetComponentInChildren<InputField>();
                componentInChildren.text = Settings.Instance.Container.LastPassword;
                componentInChildren.ActivateInputField();
            }
        }
    }

    [HarmonyPatch(typeof(ZNet), "OnPasswordEnter")]
    public class ZNetPasswordEnterHook
    {
        private static void Postfix(string pwd)
        {
            Settings.Instance.UpdateSetting(Settings.SettingTypes.LAST_PASSWORD, pwd);
        }
    }
}