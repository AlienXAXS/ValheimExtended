using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Mod.Events;

namespace Mod.Harmony.Items
{
    [HarmonyPatch(typeof(ItemDrop.ItemData), "IsEquipable")]
    public class ItemDataOverrides
    {
        private static void Postfix(ref bool __result, ItemDrop.ItemData __instance)
        {
            Utilities.Logger.Log($"IsEquipable Postfix | Game Result: {__result} | Item: {__instance.m_shared.m_name}");
            var result = EventRouter.Instance.ItemEvents.IsItemEquipableEventInvoke(__result, __instance).Result;
            Utilities.Logger.Log($"IsEquipable Postfix | Got result back from event handler: {result}");

            __result = result;

        }
    }
}
