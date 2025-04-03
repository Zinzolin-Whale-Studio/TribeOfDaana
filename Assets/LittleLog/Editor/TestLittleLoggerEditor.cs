using LittleLog.Runtime;
using UnityEditor;
using UnityEngine;
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
                Runtime.LittleLog.Log("Test Message");
            }))
            {
                text = "Log Message"
            };
            
            editorRoot.Add(logButton);

            Button warningButton = new Button((() =>
            {
                Runtime.LittleLog.LogWarning("Test Warning");
            }))
            {
                text = "Log Warning"
            };
            
            editorRoot.Add(warningButton);
            
            Button errorButton = new Button((() =>
            {
                Runtime.LittleLog.LogError("Test Error");
            }))
            {
                text = "Log Error"
            };
            
            editorRoot.Add(errorButton);
            return editorRoot;
        }
    }
}
