
using UnityEngine;

namespace LittleLog.Runtime
{
    public static class LittleLog
    {
        private static readonly ILogger logger = new LittleLogger(new LittleLogHandler());
        
        public static void Log(object message) => logger.Log(LogType.Log, message);
        public static void LogWarning(object message) => logger.Log(LogType.Warning, message);
        public static void LogError(object message) => logger.Log(LogType.Error, message);
    }
}
