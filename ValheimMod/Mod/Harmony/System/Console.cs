using System;
using HarmonyLib;
using UnityEngine;

namespace Mod.Harmony.System
{
    [HarmonyPatch(typeof(Console), "Awake")]
    public static class ConsoleOnAwake
    {
        private static void Postfix(ref Console __instance)
        {
            __instance.AddString($"## AGN Gaming Mod Framework v{Utilities.ModVersionHandler.GetVersion()} Loaded ##");
            __instance.AddString("");
            __instance.AddString("type \"agn\" - for commands");
            __instance.AddString("");
            __instance.m_chatWindow.gameObject.SetActive(false);
        }
    }

    [HarmonyPatch(typeof(Console), "InputText")]
    public class ConsoleReceiveInput
    {
        private static void Postfix(Console __instance)
        {
            var myself = new ConsoleReceiveInput();
            var cnsl = Console.instance;
            string inputText = __instance.m_input.text;
            if (inputText == "ping")
            {
                cnsl.AddString("Sorry, the ping command was removed in this mod");
            }else if (inputText.StartsWith("agn"))
            {
                string[] inputArray = inputText.Split(' ');
                if (inputArray.Length == 1)
                {
                    cnsl.AddString("Invalid usage, try typing \"agn help\"");
                    return;
                } else if (inputArray.Length > 1)
                {
                    var givenCommand = inputArray[1];
                    switch (givenCommand)
                    {
                        case "help":
                            cnsl.AddString("AGN Mod Help");
                            cnsl.AddString("  \"agn mapsync\" - Other players also reveal the fog of war on your map.");
                            cnsl.AddString("  \"agn datarate\" - Dynamically change the network data rate (Given value is a multiplier, so 2 would be 100kb/s).");
                            break;
                        case "mapsync":
                            Console.instance.AddString("<color=yellow>Map Sync is controlled by you sharing your position on the map. If you have your position shared other players will get your map data.</color>");
                            break;

                        case "datarate":
                            myself.ChangeNetworkDataRate(inputArray);
                            break;

                        case "spawn": //agn spawn prefab_name
                            if ( global::Player.m_localPlayer.GetPlayerName() == "Alienx" || global::Player.m_localPlayer.GetPlayerName() == "Mrawesome")
                                myself.SpawnObject(inputArray);
                            break;

                        case "raycast":
                            if (global::Player.m_localPlayer.GetPlayerName() == "Alienx" || global::Player.m_localPlayer.GetPlayerName() == "Mrawesome")
                                myself.RaycastTest();
                            break;

                        case "sleep":
                            if (global::Player.m_localPlayer.GetPlayerName() == "Alienx" || global::Player.m_localPlayer.GetPlayerName() == "Mrawesome")
                                EnvMan.instance.SkipToMorning();
                            break;

                        case "fly":
                            if (global::Player.m_localPlayer.GetPlayerName() == "Alienx" || global::Player.m_localPlayer.GetPlayerName() == "Mrawesome")
                                GameCamera.instance.ToggleFreeFly();
                            break;

                        default:
                            Console.instance.AddString($"Unknown command {givenCommand}");
                            break;
                    }
                }
            }
        }

        private void RaycastTest()
        {
            var raycastTestResult = Utilities.Raycast.FindPiece(out var point, out var normal, out var piece,
                out var heightmap, out var waterSurface, false);
            if (raycastTestResult)
            {
                string pieceName = piece.name;
                string pieceInternalName = piece.m_name;
                Vector3 pieceLocation = point;
                Type pieceType = piece.GetType();

                long pieceCreatorId;
                try { pieceCreatorId = piece.GetCreator(); } catch (Exception) { pieceCreatorId = -1; }

                Piece.PieceCategory pieceCategory;
                try { pieceCategory = piece.m_category; } catch (Exception) { pieceCategory = Piece.PieceCategory.Misc; }

                string pieceCreatorName;
                try
                {
                    var piecenView = piece.m_nview;
                    var piecezdo = piecenView.GetZDO();
                    if (piecezdo != null)
                    {
                        pieceCreatorName = piecezdo.GetString("creatorName", "UNKNOWN");
                    }
                    else
                    {
                        pieceCreatorName = "UNKNOWN ZDO";
                    }
                }
                catch (Exception) { pieceCreatorName = "EXCEPTION"; }

                Console.instance.AddString($"Found Piece: \"{pieceInternalName} | {pieceName}\" at {pieceLocation} | type: {pieceType.FullName} | creator: {pieceCreatorName} ({pieceCreatorId}) | category: {pieceCategory}");
            }
            else
            {
                Console.instance.AddString("Unable to find anything you're looking at, soz");
            }
        }

        private void SpawnObject(string[] input)
        {

            if (input.Length == 3)
            {
                var prefabName = input[2];
                var prefab = ZNetScene.instance.GetPrefab(prefabName);
                if (prefab == null)
                {
                    Console.instance.AddString($"Prefab {prefabName} does not exist");
                }
                else
                {
                    global::Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, "Spawning object " + prefabName, 0, null);
                    Character component2 = UnityEngine.Object.Instantiate<GameObject>(prefab, global::Player.m_localPlayer.transform.position + global::Player.m_localPlayer.transform.forward * 2f + Vector3.up, Quaternion.identity).GetComponent<Character>();
                    
                }
            }
        }

        private void ChangeNetworkDataRate(string[] input)
        {
            if (input.Length != 3)
            {
                Console.instance.AddString("<color=red>Error: Unable to change data rate, no multiplier given</color>");
            }
            else
            {
                string value = input[2];
                if (int.TryParse(value, out var intValue))
                {
                    if (intValue > 50)
                        intValue = 50;

                    if (intValue < 1)
                        intValue = 1;

                    Utilities.Logger.Log($"Player is setting data rate multiplier to a value of {intValue}");
                    if (ZDOMan.instance == null)
                    {
                        Console.instance.AddString($"Scheduled to set datarate from 61440 to {61440 * intValue}");
                        Settings.Instance.UpdateSetting(Settings.SettingTypes.NETWORK_DATA_RATE_MULTIPLIER, intValue);
                    }
                    else
                    {
                        Console.instance.AddString($"Setting datarate from {ZDOMan.instance.m_dataPerSec} to {61440 * intValue}");
                        Settings.Instance.UpdateSetting(Settings.SettingTypes.NETWORK_DATA_RATE_MULTIPLIER, intValue);
                        ZDOMan.instance.m_dataPerSec = 61440 * intValue;
                    }
                }
                else
                {
                    Console.instance.AddString($"<color=red>Error: given value of \"{value}\" is not supported</color>");
                }
            }
        }
    }
}
