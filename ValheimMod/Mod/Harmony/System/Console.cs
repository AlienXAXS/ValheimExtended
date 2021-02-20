using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace Mod.Harmony.System
{
    [HarmonyPatch(typeof(Console), "Awake")]
    public static class ConsoleOnAwake
    {
        private static void Postfix(Console __instance)
        {
            __instance.Print($"## AGN Gaming Mod Framework v{Utilities.ModVersionHandler.GetVersion()} Loaded ##");
            __instance.Print("");
            __instance.Print("type \"agn\" - for commands");
            __instance.Print("");
            __instance.m_chatWindow.gameObject.SetActive(false);
        }
    }

    [HarmonyPatch(typeof(Console), "InputText")]
    public static class ConsoleReceiveInput
    {
        private static void Postfix(Console __instance)
        {
            string inputText = __instance.m_input.text;
            if (inputText.StartsWith("agn"))
            {
                string[] inputArray = inputText.Split(' ');
                if (inputArray.Length == 1)
                {
                    Console.instance.Print("Invalid usage, try typing \"agn help\"");
                    return;
                } else if (inputArray.Length > 1)
                {
                    var givenCommand = inputArray[1];
                    switch (givenCommand)
                    {
                        case "help":
                            Console.instance.Print("AGN Mod Help");
                            Console.instance.Print("  \"agn mapsync\" - Other players also reveal the fog of war on your map.");
                            break;
                        case "mapsync":
                            Console.instance.Print("<color=yellow>Map Sync is controlled by you sharing your position on the map. If you have your position shared other players will get your map data.</color>");
                            break;

                        default:
                            Console.instance.Print($"Unknown command {givenCommand}");
                            break;
                    }
                }
            }
        }
    }
}
