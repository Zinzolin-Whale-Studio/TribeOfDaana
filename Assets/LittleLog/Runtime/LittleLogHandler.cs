using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LittleLog.Runtime
{
    internal sealed class LittleLogHandler : ILogHandler
    {
        public void LogException(Exception exception, Object context)
        {
        }
        
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            LittleLogDatabase.Instance.AddLogEntry(args[0].ToString());
        }
    }
}
