using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Events;
using Mod.Utilities;

namespace Mod.CustomCode
{
    class MapSharingMode
    {
        public void Init()
        {
            EventRouter.Instance.PlayerSpawned += OnPlayerSpawned;
        }

        private Task OnPlayerSpawned(Player player)
        {
            var logLine = $"Player respawn detected PID:{Player.m_localPlayer.GetPlayerID()}, setting options.";

            Logger.Log($"Setting SetPublicReferencePosition = {Settings.Instance.Container.RememberMapSharingMode}");
            ZNet.instance.SetPublicReferencePosition(Settings.Instance.Container.RememberMapSharingMode);

            return Task.CompletedTask;
        }
    }
}
