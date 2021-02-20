using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod
{
    public class Init
    {
        public static Init Instance = _instance ?? new Init();
        private static Init _instance;

        public void Hook()
        {
            Util.Logger.Instance.Log("Loading AGN Modpack v0.1!");
            Util.Logger.Instance.Log("Attempting to hook Harmony patches...");

            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;

            try
            {
                Settings.Instance.Init();

                var harmony = new HarmonyLib.Harmony("mod.agn.valheimmod");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Util.Logger.Instance.Log("Unable to patch with Harmony, error below");
                Util.Logger.Instance.Log($"{ex.Message}\r\n\r\n{ex.StackTrace}");
            }

            Util.Logger.Instance.Log("Patching done");
        }

        private void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Util.Logger.Instance.Log($"Engine is loading binary {args.LoadedAssembly.FullName}");
        }
    }
}
