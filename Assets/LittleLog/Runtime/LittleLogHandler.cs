using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LittleLog.Runtime
{
    public class LittleLogHandler : MonoBehaviour, ILogHandler
    {
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogException(Exception exception, Object context)
        {
            throw new NotImplementedException();
        }
    }
}
