using System;
using System.IO;
using Mod.Utilities;

namespace Mod
{
    [Serializable]
    class SettingsContainer
    {
        public bool RememberMapSharingMode;
        public int NetworkDataRateMultiplier = 1;
        public string LastIPAddress = "";
        public string LastPassword = "";
    }

    class Settings
    {
        public static Settings Instance = _instance ?? (_instance = new Settings());

        private static Settings _instance;
        private string _savePath;

        public SettingsContainer Container = new SettingsContainer();
        public enum SettingTypes
        {
            REMEMBER_MAP_SHARING_MODE,
            NETWORK_DATA_RATE_MULTIPLIER,
            LAST_IP_ADDRESS,
            LAST_PASSWORD
        }

        public Settings()
        {

        }

        public void UpdateSetting(SettingTypes Setting, object value)
        {
            switch ( Setting )
            {
                case SettingTypes.REMEMBER_MAP_SHARING_MODE:
                    Container.RememberMapSharingMode = (bool)value;
                    break;

                case SettingTypes.NETWORK_DATA_RATE_MULTIPLIER:
                    Container.NetworkDataRateMultiplier = (int) value;
                    break;

                case SettingTypes.LAST_IP_ADDRESS:
                    if (value != null)
                        Container.LastIPAddress = (string) value;
                    else
                        Utilities.Logger.Log("Attempted to write a null value to LAST_IP_ADDRESS");
                    break;

                case SettingTypes.LAST_PASSWORD:
                    if (value != null)
                        Container.LastPassword = (string) value;
                    else
                        Utilities.Logger.Log("Attempted to write a null value to LAST_PASSWORD");
                    break;
            }

            SaveSettings();
        }

        // Just a touching point to get the class instanced.
        public void Init()
        {
            if (Utilities.Tools.IsServer())
            {
                _savePath = $"./settings.json";
            }
            else
            {
                _savePath = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\AppData\\LocalLow\\IronGate\\Valheim\\settings.json";
            }
            Logger.Log($"Detected settings path as {_savePath}");

            Logger.Log("Attempting to load settings.json");
            LoadSettings();
        }

        public void LoadSettings()
        {
            try
            {
                if (!System.IO.File.Exists(_savePath))
                {
                    // Create the initial save file
                    Logger.Log("Initial settings.json file not found, will create a template one now...");
                    SaveSettings();
                    return;
                };

                var jsonString = "";
                using (var streamReader = new StreamReader(_savePath))
                {
                    jsonString = streamReader.ReadToEnd();
                }

                if (jsonString == "") return;

                var newObject = UnityEngine.JsonUtility.FromJson<SettingsContainer>(jsonString);
                if (newObject == null) return;

                Container = newObject;

                // Clamp Network Data Rate Multiplier (1 -> 50)
                if (!ZNet.m_isServer)
                {
                    if (Container.NetworkDataRateMultiplier > 50)
                        Container.NetworkDataRateMultiplier = 50;
                    if (Container.NetworkDataRateMultiplier < 1)
                        Container.NetworkDataRateMultiplier = 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"LoadSettings JSON Exception: {ex.Message}\r\n\r\n{ex.StackTrace}");
                if (ex.InnerException != null)
                    Logger.Log($"Inner exception: {ex.InnerException.Message}\r\n\r\n{ex.InnerException.StackTrace}");
            }
        }

        public void SaveSettings()
        {
            try
            {
                var jsonString = UnityEngine.JsonUtility.ToJson(Container);
                using (var streamWriter = new StreamWriter(_savePath, false))
                {
                    streamWriter.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"SaveSettings JSON Exception: {ex.Message}\r\n\r\n{ex.StackTrace}");
            }
        }
    }
}
