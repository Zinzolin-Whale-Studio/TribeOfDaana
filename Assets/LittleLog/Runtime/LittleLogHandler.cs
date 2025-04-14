using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LittleLog.Runtime
{
    internal sealed class LittleLogHandler : ILogHandler
    {
        private readonly LittleLogManager _littleLogManager = new();

        public void LogException(Exception exception, Object context)
        {
        }
        
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            string log = $"[{DateTime.UtcNow:HH:mm:ss}]";

            foreach (object o in args)
            {
                log += $" {o}";
            }
            _littleLogManager.AddConsoleEntry(new LittleLogEntry(logType, log));
        }
    }
}
