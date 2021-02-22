using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Mod.Events;
using UnityEngine;
using Logger = Mod.Utilities.Logger;
using Random = System.Random;

namespace Mod.Harmony.Game
{

    [HarmonyPatch(typeof(global::Game), "Ping")]
    public static class GamePingCapture
    {
        private static bool Prefix()
        {
            ZRoutedRpc.instance.InvokeRoutedRPC("Ping", new object[]
            {
                Time.time
            });

            return false;
        }
    }

    [HarmonyPatch(typeof(global::Game), "RPC_Pong")]
    public static class GamePongCapture
    {
        public static int PingTime;

        private static bool Prefix(long sender, float time)
        {
            PingTime = (int)((Time.time - time)*1000f);
            return false;
        }

    }

    [HarmonyPatch(typeof(global::Game), "UpdateRespawn")]
    public static class GameOverride
    {

        private static readonly List<string> RandomShitToSay = new List<string>()
        {
            "I LOVE IT IN MY BUTT",
            "I AM CUMMING! Aaarrrrgggg!",
            "SCRUB MY POOP DECK",
            "QUICKLY, FIST ME AT ONCE!",
            "I AM ARRIVING *splosh noises*",
            "SIR CUNT THE 3RD HAS ARRIVED"
        };

        private static bool Prefix(ref global::Game __instance, ref float dt)
        {
            if (__instance.m_requestRespawn && __instance.FindSpawnPoint(out var vector, out var flag, dt))
            {
                if (!flag)
                {
                    __instance.m_playerProfile.SetHomePoint(vector);
                }
                __instance.SpawnPlayer(vector);
                __instance.m_requestRespawn = false;
                if (__instance.m_firstSpawn)
                {
                    __instance.m_firstSpawn = false;
                    EventRouter.Instance.PlayerSpawnedInvoke(global::Player.m_localPlayer);

                    Random rnd = new Random(DateTime.Now.Second);
                    Chat.instance.SendText(Talker.Type.Shout, RandomShitToSay[rnd.Next(0,RandomShitToSay.Count-1)]);
                }
                GC.Collect();
            }

            return false;
        }
    }
}
