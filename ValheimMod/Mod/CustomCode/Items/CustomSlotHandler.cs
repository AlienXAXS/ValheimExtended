using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Ty to nearbear for their work on CustomSlots, it was good to learn from :)

namespace Mod.Harmony.Items
{
    public static class CustomSlotHandler
    {
        public static readonly Dictionary<Humanoid, Dictionary<string, ItemDrop.ItemData>> CustomSlotMemory = new Dictionary<Humanoid, Dictionary<string, ItemDrop.ItemData>>();

        public static void SetItemToCustomSlot(GameObject prefab, string slotName)
        {
            if (!prefab.GetComponent<CustomItemSlot>())
                prefab.AddComponent<CustomItemSlot>();

            prefab.GetComponent<CustomItemSlot>().SlotName = "wishbone";
            prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.None;
        }

        public static void SetSlotItem(Humanoid humanoid, string slotName, ItemDrop.ItemData item)
        {
            CustomSlotMemory[humanoid][slotName] = item;
        }
        
        #region Utiltiies

        public static bool DoesSlotExist(Humanoid humanoid, string slotName) =>  CustomSlotMemory[humanoid] != null && CustomSlotMemory[humanoid].ContainsKey(slotName);
        public static string GetSlotName(ItemDrop.ItemData item) => item.m_dropPrefab.GetComponent<CustomItemSlot>().SlotName;
        public static bool IsItemCustom(ItemDrop.ItemData item) => item.m_dropPrefab.GetComponent<CustomItemSlot>();
        public static bool IsSlotUsed(Humanoid humanoid, string slotName) =>
            CustomSlotMemory[humanoid] != null && CustomSlotMemory[humanoid].ContainsKey(slotName) &&
            CustomSlotMemory[humanoid][slotName] != null;

        #endregion

    }
}
