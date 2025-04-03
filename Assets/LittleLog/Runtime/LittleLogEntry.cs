using System;
using UnityEngine;

namespace LittleLog.Runtime
{
    [Serializable]
    public struct LittleLogEntry
    {
        public LogType LogType;
        public string Text;
        
        public LittleLogEntry(LogType logType, string text)
        {
            LogType = logType;
            Text = text;
        }
    }
}
