using HarmonyLib;

namespace Mod.Harmony.UI.Inventory
{
    [HarmonyPatch(typeof(InventoryGui), "UpdateRepair")]
    public class AutoRepair
    {

        private static void Prefix(InventoryGui __instance)
        {
            while (__instance.HaveRepairableItems())
            {
                __instance.RepairOneItem();
            }
        }

    }
}
