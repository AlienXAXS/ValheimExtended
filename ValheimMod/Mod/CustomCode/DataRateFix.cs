using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mod.Events;

namespace Mod.CustomCode
{
    public class DataRateFix
    {
        public void Init(bool serverOnly = false)
        {
            if (!serverOnly)
                EventRouter.Instance.PlayerSpawned += OnPlayerSpawned;

            EventRouter.Instance.ServerStarted += OnServerStarted;
        }

        private Task OnServerStarted()
        {
            Utilities.Logger.Log("Server has started, patching network bandwidth rates.");
            ConfigureDataFix();
            return Task.CompletedTask;
        }

        private Task OnPlayerSpawned(Player player)
        {
            ConfigureDataFix();
            return Task.CompletedTask;
        }

        private void ConfigureDataFix()
        {
            var intValue = Settings.Instance.Container.NetworkDataRateMultiplier;
            
            // Dont clamp the upper limit on servers.
            if (!Utilities.Tools.IsServer())
            {
                if (intValue > 50)
                    intValue = 50;
            }

            if (intValue < 1)
                intValue = 1;

            if (ZDOMan.instance == null)
            {
                Utilities.Logger.Log($"Scheduled to set datarate from 61440 to {61440 * intValue}");
                Settings.Instance.UpdateSetting(Settings.SettingTypes.NETWORK_DATA_RATE_MULTIPLIER, intValue);
            }
            else
            {
                Utilities.Logger.Log($"Setting datarate from {ZDOMan.instance.m_dataPerSec} to {61440 * intValue}");
                Settings.Instance.UpdateSetting(Settings.SettingTypes.NETWORK_DATA_RATE_MULTIPLIER, intValue);
                ZDOMan.instance.m_dataPerSec = 61440 * intValue;
            }
        }
    }
}
