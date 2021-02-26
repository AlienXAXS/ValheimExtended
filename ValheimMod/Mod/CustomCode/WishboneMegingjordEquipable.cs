using System.Threading.Tasks;
using Mod.Events;
using Mod.Harmony.Items;

namespace Mod.CustomCode
{
    class WishboneMegingjordEquipable
    {
        public void Init()
        {
            //EventRouter.Instance.GameReady += OnGameReady;
            EventRouter.Instance.ItemEvents.IsItemEquipable += OnItemIsEquipable;
        }

        private bool OnItemIsEquipable(bool result, ItemDrop.ItemData itemData)
        {
            Utilities.Logger.Log($"OnItemIsEquipable | Result from game: {result} | Item: {itemData.m_shared.m_name.ToLower()}");
            return itemData.m_shared.m_name.ToLower() == "$item_wishbone" || result;
        }

        private Task OnGameReady()
        {
            var prefab = ZNetScene.instance.GetPrefab("Wishbone");
            if (prefab)
            {
                CustomSlotHandler.SetItemToCustomSlot(prefab, "wishbone");
            }

            return Task.CompletedTask;
        }
    }
}
