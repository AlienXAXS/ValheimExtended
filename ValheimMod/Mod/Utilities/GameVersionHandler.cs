using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;

namespace Mod.Utilities
{
    public class GameVersionHandler
    {
        public static GameVersionHandler Instance = _instance ?? (_instance = new GameVersionHandler());
        private static GameVersionHandler _instance;

        private string _gameVersion = "UNKNOWN";
        public string GameVersion
        {
            get => _gameVersion;
            set
            {
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (value.Contains("version"))
                {
                    _gameVersion = value.Split(' ')[1];
                }
                else
                {
                    _gameVersion = value;
                }

                Console.instance.Print($"AGN Mods | Game version detected: {_gameVersion}");
            }
        }
    }
}
