using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.Log
{
    public static class ToDLog
    {
        private const string _logPrefix = "[<color=lightblue><b>ToD</b></color>] ";
        
        public static void Log(string message)
        {
            Debug.Log($"{_logPrefix}" + message);
        }
    }
}
