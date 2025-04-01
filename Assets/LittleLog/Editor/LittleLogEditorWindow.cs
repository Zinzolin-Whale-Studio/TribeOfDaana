using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LittleLog.Editor
{
    using Runtime;
    public class LittleLogEditorWindow : EditorWindow
    {
        [MenuItem("Tools/Little Log Console")]
        public static void Open()
        {
            LittleLogEditorWindow[] windows = Resources.FindObjectsOfTypeAll<LittleLogEditorWindow>();

            if (windows.Length > 0)
            {
                windows[0].Focus();
                return;
            }

            Assembly assembly = typeof(UnityEditor.Editor).Assembly;
            Type consoleWindowType = assembly.GetType("UnityEditor.ConsoleWindow");
            LittleLogEditorWindow window = GetWindow<LittleLogEditorWindow>(consoleWindowType);
            window.titleContent = new GUIContent("Little Log Console");
        }

        private void OnEnable()
        {
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            LittleLogDatabase.Instance.EntryAdded += OnEntryAdded;
        }
        
        private void CreateGUI()
        {
        }

        #region React to LittleLogDatabase events

        private void OnEntryAdded(LittleLogEntry entry)
        {
            LittleLogEditorEntry editorEntry = new LittleLogEditorEntry(entry.Text);
            rootVisualElement.Add(editorEntry);
        }

        #endregion
        

        

        private void OnDisable()
        {
            
        }
    }
}
