using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Mod.Events;

namespace Mod.Harmony.Camera
{
    [HarmonyPatch(typeof(GameCamera), "Awake")]
    public class GameCameraAwakeHook
    {
        private static void Postfix(GameCamera __instance)
        {
            EventRouter.Instance.GameCameraEvents.CameraAwakeEventInvoke(__instance);
        }
    }

    [HarmonyPatch(typeof(GameCamera), "UpdateCamera")]
    public class GameCameraHook
    {
        private static void Postfix(GameCamera __instance, float dt)
        {
            EventRouter.Instance.GameCameraEvents.CameraUpdateEventInvoke(__instance, dt);
        }
    }
}