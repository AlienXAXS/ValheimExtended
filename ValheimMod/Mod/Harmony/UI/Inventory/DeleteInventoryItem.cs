using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Mod.Harmony.UI.Inventory
{
    [HarmonyPatch(typeof(InventoryGui), "UpdateItemDrag")]
    public class DeleteInventoryItem
    {
        private static void Postfix(InventoryGui __instance)
        {
            if (!Input.GetKeyDown(KeyCode.Delete) || __instance.m_dragItem == null) return;
            if (!__instance.m_dragInventory.ContainsItem(__instance.m_dragItem)) return;

            // Are we dragging a full stack?
            if (__instance.m_dragAmount == __instance.m_dragItem.m_stack)
            {
                __instance.m_dragInventory.RemoveItem(__instance.m_dragItem);
            }
            else
            {
                __instance.m_dragInventory.RemoveItem(__instance.m_dragItem, __instance.m_dragAmount);
            }

            Object.Destroy(__instance.m_dragGo);
            __instance.m_dragGo = null;
            __instance.UpdateCraftingPanel(false);
        }
    }
}
