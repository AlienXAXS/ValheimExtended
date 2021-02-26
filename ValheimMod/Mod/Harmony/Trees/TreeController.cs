using HarmonyLib;

namespace Mod.Harmony.Trees
{
    [HarmonyPatch(typeof(TreeBase), "Damage")]
    public static class TreeControllerDamage
    {
        private static void Postfix(TreeBase __instance)
        {

        }
    }

    public static class TreeControllerRPCDamage
    {
        private static void Postfix(TreeBase __instance)
        {

        }
    }
}
