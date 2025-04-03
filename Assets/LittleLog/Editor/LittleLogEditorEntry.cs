using LittleLog.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace LittleLog.Editor
{
    internal class LittleLogEditorEntry : VisualElement
    {
        public LittleLogEditorEntry(LittleLogEntry entry)
        {
            style.width = new StyleLength(Length.Percent(100));
            style.height = 40;
            style.paddingLeft = 5;
            style.paddingRight = 5;
            style.paddingTop = 5;
            style.paddingBottom = 5;
            style.flexDirection = FlexDirection.Row;

            Texture2D texture = null;
            switch (entry.LogType)
            {
                case LogType.Log:
                    texture = Resources.Load<Texture2D>(LittleLogEditorResources.MessageIconPath);
                    break;
                case LogType.Warning:
                    texture = Resources.Load<Texture2D>(LittleLogEditorResources.WarningIconPath);
                    break;
                case LogType.Error:
                    texture = Resources.Load<Texture2D>(LittleLogEditorResources.ErrorIconPath);
                    break;
                default:
                    texture = Resources.Load<Texture2D>(LittleLogEditorResources.MessageIconPath);
                    break;
            }

            Image iconImage = new Image()
            {
                style =
                {
                    width = 32,
                    height = 32
                }
            };

            if (texture != null)
            {
                iconImage.image = texture;
            }
            Add(iconImage);
            
            Label textLabel = new Label()
            {
                text = entry.Text
            };
            
            Add(textLabel);
        }
    }
}
