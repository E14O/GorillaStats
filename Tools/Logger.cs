using BepInEx.Logging;

// Credits: H4RNS
namespace GorillaServerStats.Tools
{
    internal class Logger
    {
        private static ManualLogSource logger;

        private static ManualLogSource Instance => logger ??= BepInEx.Logging.Logger.CreateLogSource(Constants.PluginName);

        public static void LogInfo(string message) => Instance.LogInfo(message);
        public static void LogMessage(string message) => Instance.LogMessage(message);
        public static void LogWarning(string message) => Instance.LogWarning(message);
        public static void LogError(string message) => Instance.LogError(message);
    }
}
