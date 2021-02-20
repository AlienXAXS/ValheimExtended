using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mod
{
    [Serializable]
    class SettingsContainer
    {
        public bool RememberMapSharingMode;
    }

    class Settings
    {
        public static Settings Instance = _instance ?? (_instance = new Settings());

        private static Settings _instance;
        private readonly string _savePath;

        public SettingsContainer Container = new SettingsContainer();
        public enum SettingTypes
        {
            REMEMBER_MAP_SHARING_MODE
        }

        public Settings()
        {
            // TODO: Figure out if Linux or not, if linux save along side binary
            _savePath = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\AppData\\LocalLow\\IronGate\\Valheim\\settings.json";
            Util.Logger.Instance.Log($"Detected settings path as {_savePath}");
            Util.Logger.Instance.Log("Attempting to load settings.json");
            LoadSettings();
        }

        public void UpdateSetting(SettingTypes Setting, object value)
        {
            switch ( Setting )
            {
                case SettingTypes.REMEMBER_MAP_SHARING_MODE:
                    Container.RememberMapSharingMode = (bool)value;
                    break;
            }

            SaveSettings();
        }

        // Just a touching point to get the class instanced.
        public void Init()
        { }

        public void LoadSettings()
        {
            try
            {
                if (!System.IO.File.Exists(_savePath))
                {
                    // Create the initial save file
                    Util.Logger.Instance.Log("Initial settings.json file not found, will create a template one now...");
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
            }
            catch (Exception ex)
            {
                Util.Logger.Instance.Log($"LoadSettings JSON Exception: {ex.Message}\r\n\r\n{ex.StackTrace}");
                if (ex.InnerException != null)
                    Util.Logger.Instance.Log($"Inner exception: {ex.InnerException.Message}\r\n\r\n{ex.InnerException.StackTrace}");
            }
        }

        public void SaveSettings()
        {
            try
            {
            
                Util.Logger.Instance.Log("Attempting serialisation...");
                var jsonString = UnityEngine.JsonUtility.ToJson(Container);
                Util.Logger.Instance.Log($"Done, output is:\r\n{jsonString}");
                using (var streamWriter = new StreamWriter(_savePath, false))
                {
                    streamWriter.Write(jsonString);
                }
                Util.Logger.Instance.Log("File written");
            }
            catch (Exception ex)
            {
                Util.Logger.Instance.Log($"SaveSettings JSON Exception: {ex.Message}\r\n\r\n{ex.StackTrace}");
            }
        }
    }
}
