/*
	Original Source: https://github.com/nxPublic/ValheimPlus/blob/master/ValheimPlus/SharedMapSystem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace Mod.Harmony.Map
{
    [HarmonyPatch(typeof(Minimap))]
    public class hookExplore
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(Minimap), "Explore", new Type[] { typeof(Vector3), typeof(float) })]
        public static void call_Explore(object instance, Vector3 p, float radius) => throw new NotImplementedException();
    }
    [HarmonyPatch(typeof(ZNet))]
    public class hookZNet
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(ZNet), "GetOtherPublicPlayers", new Type[] { typeof(List<ZNet.PlayerInfo>) })]
        public static void GetOtherPublicPlayers(object instance, List<ZNet.PlayerInfo> playerList) => throw new NotImplementedException();

    }

    [HarmonyPatch(typeof(Minimap), "UpdateExplore")]
    public static class ChangeMapBehavior
    {

        private static void Prefix(ref float dt, ref Player player, ref Minimap __instance, ref float ___m_exploreTimer, ref float ___m_exploreInterval, ref List<ZNet.PlayerInfo> ___m_tempPlayerInfo) // Set after awake function
        {
            float exploreRadius = 100f;
            float explorerTime = ___m_exploreTimer;
            explorerTime += Time.deltaTime;
            if (explorerTime > ___m_exploreInterval)
            {
                ___m_tempPlayerInfo.Clear();
                hookZNet.GetOtherPublicPlayers(ZNet.instance, ___m_tempPlayerInfo); // inconsistent returns but works

                if (___m_tempPlayerInfo.Any())
                {
                    foreach (ZNet.PlayerInfo m_Player in ___m_tempPlayerInfo)
                    {
                        if ( m_Player.m_publicPosition )
                            hookExplore.call_Explore(__instance, m_Player.m_position, exploreRadius);
                    }
                }
            }

            hookExplore.call_Explore(__instance, player.transform.position, exploreRadius);
        }
    }
}
