using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Mod.Harmony.DungeonDB
{
    [HarmonyPatch(typeof(global::FejdStartup), "Start")]
    public class FejdStartupHook
    {
        private static void Postfix()
        {
            Events.EventRouter.Instance.GameEvents.GameReadyInvoke();
        }
    }
}
