using UnityEngine;

namespace LittleLog.Editor
{
    internal static class LittleLogEditorResources
    {
        // Those path were written considering we use them for Resources.Load.
        // So we don't add the file extension at the end.
        public static readonly Texture2D MessageIcon =  Resources.Load<Texture2D>("Icons/icon_message");
        public static readonly Texture2D WarningIcon = Resources.Load<Texture2D>("Icons/icon_warning");
        public static readonly Texture2D ErrorIcon = Resources.Load<Texture2D>("Icons/icon_error");
    }
}
