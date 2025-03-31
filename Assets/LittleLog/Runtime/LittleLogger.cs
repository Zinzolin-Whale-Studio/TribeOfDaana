using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LittleLog.Runtime
{
    internal class LittleLogger : Logger,  ILogger, ILogHandler
    {
        public LittleLogger(ILogHandler inLogHandler) : base(inLogHandler)
        {
            logHandler = inLogHandler;
            logEnabled = true;
            filterLogType = LogType.Log;
        }
    }
}
