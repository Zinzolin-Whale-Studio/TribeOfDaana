using System;
using UnityEngine;

namespace LittleLog.Runtime
{
    public struct LittleLogEntry
    {
        public string Text;
        
        public LittleLogEntry(string text)
        {
            Text = text;
        }
    }
}
