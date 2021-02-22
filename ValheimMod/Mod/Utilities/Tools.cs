using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod.Utilities
{
    public static class Tools
    {
        public static bool IsServer()
        {
            var cmdArgs = Environment.GetCommandLineArgs();
            var xx = string.Join(",", cmdArgs);
            return cmdArgs.Any(x => x.ToLower() == "-nographics") && cmdArgs.Any(x => x.ToLower() == "-password");
        }

    }
}
