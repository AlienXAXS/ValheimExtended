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

    [HarmonyPatch(typeof(Terminal), "InputText")]
    public class ConsoleReceiveInput
    {
        private static void Postfix(Terminal __instance)
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
                        case "spawn": //agn spawn prefab_name
                            myself.SpawnObject(inputArray);
                            break;

                        case "raycast":
                            myself.RaycastTest();
                            break;

                        case "sleep":
                            EnvMan.instance.SkipToMorning();
                            break;

                        case "fly":
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
    }
}
