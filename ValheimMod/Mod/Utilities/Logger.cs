using System;

namespace Mod.Utilities
{
    public static class Logger
    {
        private static readonly object _lockable = new object();

        public static void RestartLog()
        {
            Log("Logger init", append: false);
        }

        private static void WriteTextToFile(string input, bool append = true)
        {
            UnityEngine.Debug.Log(input);
        }

        private static void WriteTextToConsole(string input, string color = "white")
        {
            if (Console.instance == null) return;

            Console.instance.AddString($"<color={color}>{input}</color>");
        }

        public static void Log(string message, string color = "white", Exception exception = null, bool append = true)
        {
            lock (_lockable)
            {
                var dt = DateTime.Now;
                var compiledMessage = $"AlienX's Mod: [{dt.Year}/{dt.Month}/{dt.Day} {dt.Hour}:{dt.Minute}:{dt.Second}] | {message}";

                try
                {
                    WriteTextToConsole(message, color);
                    WriteTextToFile($"{compiledMessage}{Environment.NewLine}", append:append);

                    if (exception != null)
                    {
                        WriteTextToFile($"{exception}{Environment.NewLine}", append:append);
                        if ( exception.InnerException != null )
                            WriteTextToFile($"{exception.InnerException}{Environment.NewLine}", append: append);
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.Log(compiledMessage);
                    UnityEngine.Debug.LogException(ex);
                }
            }
        }
    }
}
