using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Mod.Events;

namespace Mod.Harmony.Network
{
    [HarmonyPatch]
    public class DataRatePatcher
    {
        [HarmonyPatch(typeof(ZDOMan), "ResetSectorArray")]
        private static void Prefix()
        {
            EventRouter.Instance.GameReady += () =>
            {
                var newDataRate = DefaultDataRate * Settings.Instance.Container.NetworkDataRateMultiplier;
                Utilities.Logger.Log($"Network data rate changed from {DefaultDataRate} to {newDataRate}");

                ZDOMan.instance.m_dataPerSec = newDataRate;

                return Task.CompletedTask;
            };
        }

        private const int DefaultDataRate = 61440;
    }
}
