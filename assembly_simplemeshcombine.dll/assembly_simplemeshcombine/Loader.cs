using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Mod.Utilities;

namespace assembly_simplemeshcombine
{
    // AGN Valheim Loader
    public class Loader
    {
        public static Loader Instance = _instance ?? (_instance = new Loader());
        private static Loader _instance;

        private bool isLoaded;

        public void Inject()
        {
            if (isLoaded) return;
            isLoaded = true;

            UnityEngine.Debug.Log("Hello, i r here - wooo");

            Logger.RestartLog();
            Logger.Log("Got pre-hook init, loading mod modules now...");
            Mod.Init.Instance.Hook();
        }
    }
}
