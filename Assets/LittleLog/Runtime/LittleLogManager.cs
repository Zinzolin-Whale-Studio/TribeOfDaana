using UnityEngine;

namespace LittleLog.Runtime
{
    internal class LittleLogManager
    {
        public void AddConsoleEntry(LittleLogEntry entry)
        {
            LittleLogConsoleDatabase.Instance.AddLogEntry(entry);
        }
    }
}
