namespace Mod.Utilities
{
    public static class ModVersionHandler
    {
        public static string GetVersion()
        {
            return typeof(ModVersionHandler).Assembly.GetName().Version.ToString();
        }
    }
}
