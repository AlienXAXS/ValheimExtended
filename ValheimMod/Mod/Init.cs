using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Mod.CustomCode;
using Mod.Events;
using UnityEngine;
using Logger = Mod.Utilities.Logger;

namespace Mod
{
    public class Init
    {
        public static Init Instance = _instance ?? (_instance = new Init());
        private static Init _instance;

        public void Hook()
        {
            Logger.Log("Loading AGN Modpack v0.1!", "green");
            Logger.Log("Attempting to hook Harmony patches", "green");

            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;

            try
            {
                var harmony = new HarmonyLib.Harmony("mod.agn.valheimmod");
                harmony.PatchAll();

                Logger.Log("Hooking into DungeonDB Awake");
                EventRouter.Instance.GameReady += GameIsReadyEvent;
                Logger.Log("Hook complete, waiting for DungeonDB to be awake.");
            }
            catch (Exception ex)
            {
                Logger.Log("Unable to patch with Harmony, error follows", "red", ex);
            }

            Logger.Log("Patching done");
        }

        private Task GameIsReadyEvent()
        {
            Utilities.Logger.Log("GameIsReadyEvent detected DungeonDB startup, firing events!");

            Settings.Instance.Init();

            // Don't load the rest on the server, our hooks are enough
            if ( ZNet.m_isServer ) return Task.CompletedTask;

            new MapSharingMode().Init();
            new FirstPersonMode().Init();

            return Task.CompletedTask;
        }

        private void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Logger.Log($"Engine is loading binary {args.LoadedAssembly.FullName}", "yellow");
        }
    }
}
