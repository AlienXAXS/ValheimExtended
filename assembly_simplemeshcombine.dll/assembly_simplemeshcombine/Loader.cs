using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace assembly_simplemeshcombine
{
    // AGN Valheim Loader
    public class Loader
    {
        public static Loader Instance = _instance ?? new Loader();
        private static Loader _instance;

        private bool isLoaded;

        public void Inject()
        {
            if (isLoaded) return;
            isLoaded = true;

            Util.Logger.Instance.Log("Got pre-hook init, loading mod modules now...");
            Mod.Init.Instance.Hook();
        }
    }
}
