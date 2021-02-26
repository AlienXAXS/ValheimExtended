using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Mod.Harmony.Player
{
    [HarmonyPatch(typeof(global::Player), "UpdateTeleport")]
    public class TeleportFix
    {
        private static void Prefix(global::Player __instance, ref float dt)
        {
            dt *= 3f;
            if (__instance.m_teleportTimer > 0f && __instance.m_teleportTimer <= 2f)
            {
                __instance.m_teleportTimer = 2f;
            }
        }
    }
}
