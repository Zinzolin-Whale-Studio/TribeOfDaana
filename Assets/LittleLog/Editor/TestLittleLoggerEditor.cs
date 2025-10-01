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
            
            Button logButton = new Button(() =>
            {
                Runtime.LittleLog.Log("Test Message");
            })
            {
                text = "Log Message"
            };
            Button logTagButton = new Button(() =>
            {
                Runtime.LittleLog.Log("[<b>LittleLog</b>]","Test Message");
            })
            {
                text = "Log Tag Message"
            };
            
            editorRoot.Add(logButton);
            editorRoot.Add(logTagButton);

            Button warningButton = new Button(() =>
            {
                Runtime.LittleLog.LogWarning("Test Warning");
            })
            {
                text = "Log Warning"
            };
            Button warningTagButton = new Button(() =>
            {
                Runtime.LittleLog.LogWarning("<color=yellow><b>[LittleLog]</b></color>","Test Warning");
            })
            {
                text = "Log Tag Warning"
            };
            
            editorRoot.Add(warningButton);
            editorRoot.Add(warningTagButton);
            
            Button errorButton = new Button(() =>
            {
                Runtime.LittleLog.LogError("Test Error");
            })
            {
                text = "Log Error"
            };
            Button errorTagButton = new Button(() =>
            {
                Runtime.LittleLog.LogError("<color=red><b>[LittleLog]</b></color>","Test Error");
            })
            {
                text = "Log Tag Error"
            };
            
            editorRoot.Add(errorButton);
            editorRoot.Add(errorTagButton);
            
            return editorRoot;
        }
    }
}
