using _TribeOfDaana.Scripts.Runtime.Core.Diagnostic;

namespace _TribeOfDaana.Scripts.Runtime.Logic.CampSceneSystem
{
    public static class CampSceneDebug
    {
        private const string c_tag = "CampSceneSystem";
        private const string c_tagColor = "orange";
        
        public static void Log(string message)
        {
            ToDDebug.Log(message, c_tag, c_tagColor);
        }
        
        public static void LogWarning(string message)
        {
            ToDDebug.LogWarning(message, c_tag, c_tagColor);
        }
        
        public static void LogError(string message)
        {
            ToDDebug.LogError(message, c_tag, c_tagColor);
        }
    }
}