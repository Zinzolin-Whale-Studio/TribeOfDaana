using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.Diagnostic
{
    public static class ToDDebug
    {
        public static void Log(string tag, string message, string tagColor = null)
        {
            
            Debug.Log(FormatMessage(tag, message, FormatTagColor(tagColor), "white"));
        }
        
        public static void LogWarning(string tag, string message, string tagColor = null)
        {
            Debug.LogWarning(FormatMessage(tag, message, FormatTagColor(tagColor), "yellow"));
        }
        
        public static void LogError(string tag, string message, string tagColor = null)
        {
            Debug.LogError(FormatMessage(tag, message, FormatTagColor(tagColor), "red"));
        }

        private static string FormatTagColor(string tagColor)
        {
            return !string.IsNullOrEmpty(tagColor) ? tagColor : "white";
        }
        
        private static string FormatMessage(string tag, string message, string tagColor = null, string messageColor = null)
        {
            string coloredTag = !string.IsNullOrEmpty(tagColor) ? $"<color={tagColor}>{tag}</color>" : $"<color=white>{tag}</color>";
            
            string coloredMessage = !string.IsNullOrEmpty(messageColor) ? $"<color={messageColor}>{message}</color>" : $"<color=white>{message}</color>";
            
            return FormatMessage(coloredTag, coloredMessage);
        }
        
        private static string FormatMessage(string tag, string message)
        {
            return $"[<b>{tag}</b>] {message}";
        }
    }
}
