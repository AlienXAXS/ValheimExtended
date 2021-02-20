using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mod.Utilities
{
    public static class ModVersionHandler
    {
        public static string GetVersion()
        {
            return typeof(ModVersionHandler).Assembly.GetName().Version.ToString();
        }
    }
}
