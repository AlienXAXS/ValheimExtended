using System;
using HarmonyLib;

namespace Mod.Harmony.Items
{
    [HarmonyPatch(typeof(Fermenter), "GetHoverText")]
    public class FermenterProgress
    {
        private static string Postfix(string __result, Fermenter __instance)
        {
            var currentFermentingContent = __instance.GetContentName();
            if (__result.Equals(Localization.instance.Localize(__instance.m_name + " ( " + currentFermentingContent +
                                               ", $piece_fermenter_fermenting )")))
            {

                DateTime d = new DateTime(__instance.m_nview.GetZDO().GetLong("StartTime", 0L));
                int result;
                if (d.Ticks == 0L)
                {
                    result = -1;
                }
                else
                {
                    result = (int)((ZNet.instance.GetTime() - d).TotalSeconds / (double)__instance.m_fermentationDuration * 100.0);
                }

                __result = Localization.instance.Localize(string.Concat(new string[]
                {
                    __instance.m_name,
                    " ( ",
                    currentFermentingContent,
                    ", $piece_fermenter_fermenting ",
                    result.ToString(),
                    "% )"
                }));
            }

            return __result;
        }
    }
}
