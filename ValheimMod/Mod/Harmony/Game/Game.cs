using System;
using System.Collections.Generic;
using HarmonyLib;
using Mod.Events;
using Random = System.Random;

namespace Mod.Harmony.Game
{
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
            "SIR TW@ THE 3RD HAS ARRIVED",
            "Excuse me, have you seen my shoes?",
            "I am here to conquer your lands (and your butt!)",
            "Good day, May I interest you in the story of our lord and savouir Jebus Chryst?",
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
                    EventRouter.Instance.PlayerEvents.PlayerSpawnedInvoke(global::Player.m_localPlayer);

                    Random rnd = new Random(DateTime.Now.Second);
                    Chat.instance.SendText(Talker.Type.Shout, RandomShitToSay[rnd.Next(0,RandomShitToSay.Count)]);
                }
                GC.Collect();
            }

            return false;
        }
    }
}
