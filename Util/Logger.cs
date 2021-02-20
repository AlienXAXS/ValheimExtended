using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Util
{
    public class Logger
    {
        public static Logger Instance = _instance ?? (_instance = new Logger());
        private static readonly Logger _instance;
        private string LogFileName = "ModLoader_Log.log";

        private readonly object _lockable = new object();

        private int _incrementValue = 0;

        public Logger()
        {
            LogFileName = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\AppData\\LocalLow\\IronGate\\Valheim\\AGNMod.log";
            WriteTextToFile(LogFileName, "Logger init", append: false);
        }

        private void WriteTextToFile(string path, string input, bool append = true)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, append, Encoding.ASCII, 1024))
                streamWriter.Write($"[Valheim AGN Mod] {input}");
        }

        public void Log(string message, Exception exception = null)
        {
            lock (_lockable)
            {
                var dt = DateTime.Now;
                var compiledMessage = $"[{dt.Year}/{dt.Month}/{dt.Day} {dt.Hour}:{dt.Minute}:{dt.Second}] | {message}";

                try
                {
                    WriteTextToFile(LogFileName, $"{compiledMessage}{Environment.NewLine}");

                    if (exception != null)
                    {
                        WriteTextToFile(LogFileName, $"{exception}{Environment.NewLine}");
                        if ( exception.InnerException != null )
                            WriteTextToFile(LogFileName, $"{exception.InnerException}{Environment.NewLine}");
                    }
                }
                catch (Exception ex)
                {
                    // what do now?
                }
            }
        }
        
        public void IncrementLogValue(string tag, int value = -1)
        {
            if ( value != -1 )
                _incrementValue = value;

            Log($"< IncrementValue | {tag} > {_incrementValue}");

            _incrementValue++;
        }
    }
}
