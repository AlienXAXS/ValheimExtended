using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Mod.Events;

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
                    EventRouter.Instance.ServerStartedInvoke();
                }
            }
        }
    }
}