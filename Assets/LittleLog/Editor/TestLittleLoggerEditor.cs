using LittleLog.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace LittleLog.Editor
{
    [CustomEditor(typeof(TestLittleLogger))]
    public class TestLittleLoggerEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement editorRoot = new VisualElement();
            
            VisualElement defaultEditor =  base.CreateInspectorGUI();
            
            editorRoot.Add(defaultEditor);
            
            Button logButton = new Button((() =>
            {
                Runtime.LittleLog.Log("Test Log");
            }))
            {
                text = "Log"
            };
            
            editorRoot.Add(logButton);

            return editorRoot;
        }
    }
}
