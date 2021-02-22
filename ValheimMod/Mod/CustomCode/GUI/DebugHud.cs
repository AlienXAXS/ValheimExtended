using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod.CustomCode.GUI
{
    public class DebugHud : MonoBehaviour
    {

        private bool _firstRun = true;

        private float timeRemaining = 10;
        private const float defaultTime = 10;

        private Vector3 playerPosition = Vector3.zero;

        private string totalBytesSent;
        private string totalBytesRecv;

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
                    ZNet.instance.GetNetStats(out var totalSent, out var totalRecv);
                    totalBytesSent = $"{totalSent / 1024}";
                    totalBytesRecv = $"{totalRecv / 1024}";
                }
            }
            else
            {
                timeRemaining = defaultTime;
                if (ZRoutedRpc.instance != null)
                {
                    Game.instance.Ping();
                }
            }
        }

        private void OnGUI()
        {
            //GUILayout.Window(133723, new Rect(7f, 10f, Screen.width - 10f, 20f), new UnityEngine.GUI.WindowFunction(GUIWindowFunctionHandler), "", new GUILayoutOption[0]);
            GUILayout.Label($"<color=white>Ping: {Harmony.Game.GamePongCapture.PingTime}ms | Pos: {playerPosition} | Net: {totalBytesSent}kb/s out {totalBytesRecv}kb/s in</color>", new GUIStyle() { margin = new RectOffset(15, 5, 5, 5), fontSize = 16}, new GUILayoutOption[0]);
        }

        private void GUIWindowFunctionHandler(int windowId)
        {
            Utilities.Logger.Log("TestGUI GUIWindowFunctionHandler");
            try
            {

                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label("This is a big fat test label, wooo", new GUILayoutOption[0]);

            }
            catch (Exception ex)
            {
                Utilities.Logger.Log("Error while attempting to do GUI work", "red", ex);
            }
        }
    }
}
