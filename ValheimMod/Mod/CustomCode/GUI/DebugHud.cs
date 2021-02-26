using System;
using UnityEngine;

namespace Mod.CustomCode.GUI
{
    public class DebugHud : MonoBehaviour
    {
        private float timeRemaining = 10;
        private const float defaultTime = 10;

        private Vector3 playerPosition = Vector3.zero;

        private string totalBytesSent;
        private string totalBytesRecv;
        private int Ping;
        private float localNetQuality;
        private float remoteNetQuality;

        private void Start()
        {
            Utilities.Logger.Log("TestGUI START");
        }

        private void Update()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (Player.m_localPlayer != null)
                    playerPosition = Player.m_localPlayer.transform.position;

                if (ZNet.instance != null)
                {
                    try
                    {
                        ZNet.instance.GetNetStats(out var localQuality, out var remoteQuality, out int PingStat, out var totalSent, out var totalRecv);
                        totalBytesSent = $"{(int)totalSent / 1024}";
                        totalBytesRecv = $"{(int)totalRecv / 1024}";
                        Ping = PingStat;

                        localNetQuality = localQuality;
                        remoteNetQuality = remoteQuality;
                    }
                    catch (Exception)
                    {
                        totalBytesSent = "?";
                        totalBytesRecv = "?";
                    }
                }
            }
            else
            {
                timeRemaining = defaultTime;
                if (ZRoutedRpc.instance != null)
                {
                    //Game.instance.Ping();
                }
            }
        }

        private void OnGUI()
        {
            //GUILayout.Window(133723, new Rect(4f, 16f, Screen.width - 10f, 26f), new UnityEngine.GUI.WindowFunction(GUIWindowFunctionHandler), "", new GUILayoutOption[0]);
            GUILayout.Label($"<color=white>Ping: {Ping}ms | Pos: {playerPosition} | Network: Transmit:{totalBytesRecv}kb/s Recieve:{totalBytesSent}kb/s LocalQuality:{(int)localNetQuality * 100f}% RemoteQuality:{(int)remoteNetQuality * 100f}%</color>", new GUIStyle() { margin = new RectOffset(15, 5, 5, 5), fontSize = 16 }, new GUILayoutOption[0]);
        }

        private void GUIWindowFunctionHandler(int windowId)
        {
            Utilities.Logger.Log("TestGUI GUIWindowFunctionHandler");
            try
            {

                GUILayout.BeginHorizontal(new GUILayoutOption[0]);

            }
            catch (Exception ex)
            {
                Utilities.Logger.Log("Error while attempting to do GUI work", "red", ex);
            }
        }
    }
}
