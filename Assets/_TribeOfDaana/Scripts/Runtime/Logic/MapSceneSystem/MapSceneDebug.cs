using _TribeOfDaana.Scripts.Runtime.Core.Diagnostic;

namespace _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem
{
    public static class MapSceneDebug
    {
        private const string c_tag = "MapSceneSystem";
        private const string c_tagColor = "lightblue";
        
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
