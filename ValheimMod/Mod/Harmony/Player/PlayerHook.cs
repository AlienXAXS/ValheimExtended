using System.Linq;
using HarmonyLib;
using Mod.Events;
using UnityEngine;

namespace Mod.Harmony.Player
{
    [HarmonyPatch(typeof(global::Player), "Update")]
    public class PlayerHook
    {
        private static void Postfix(global::Player __instance)
        {
            if (!__instance.m_nview.IsValid())
            {
                return;
            }
            if (!__instance.m_nview.IsOwner())
            {
                return;
            }

            EventRouter.Instance.PlayerEvents.PlayerUpdateEventInvoke(__instance);

            bool acceptInput = __instance.TakeInput();
            if (acceptInput)
            {
                foreach (var key in InputKeyRouter.Instance.GetKeyCodes().Where(Input.GetKeyDown))
                {
                    InputKeyRouter.Instance.KeyDownInvoke(key);
                }
            }
        }
    }
}
