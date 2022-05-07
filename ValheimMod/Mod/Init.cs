using System;
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

        private bool hasLoaded = false;

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
                EventRouter.Instance.GameEvents.GameReady += GameIsReadyEvent;
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
            if ( hasLoaded ) return Task.CompletedTask;
            hasLoaded = true;

            Utilities.Logger.Log("GameIsReadyEvent detected game startup, firing events!");

            Settings.Instance.Init();

            // Don't load the rest on the server, our hooks are enough
            Utilities.Logger.Log($"Are we a server? {Utilities.Tools.IsServer()}");

            if (Utilities.Tools.IsServer())
            {
            }
            else
            {
                new MapSharingMode().Init();
                new FirstPersonMode().Init();
                new WishboneMegingjordEquipable().Init();

                EventRouter.Instance.PlayerEvents.PlayerSpawned += player =>
                {
                    var go = new GameObject("Mod");
                    UnityEngine.Object.DontDestroyOnLoad(go);
                    go.AddComponent<CustomCode.GUI.DebugHud>();

                    return Task.CompletedTask;
                };
            }

            return Task.CompletedTask;
        }

        private void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Logger.Log($"Engine is loading binary {args.LoadedAssembly.FullName}", "yellow");
        }
    }
}
