using UnityEngine.UIElements;

namespace LittleLog.Editor
{
    public class LittleLogEditorEntry : VisualElement
    {
        private string _text;
        
        public LittleLogEditorEntry(string inText)
        {
            _text = inText;

            Label textLabel = new Label()
            {
                text = _text
            };
            
            Add(textLabel);
        }
    }
}
