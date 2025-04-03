using System;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Assembly = System.Reflection.Assembly;

namespace LittleLog.Editor
{
    using Runtime;
    public class LittleLogEditorWindow : EditorWindow
    {
        private VisualElement _entriesContainer;
        private bool _isNextEntryEven = false;
        
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

        private void InitializeWindow()
        {
            _isNextEntryEven = false;
            
            LittleLogConsoleDatabase.Instance.EntryAdded += OnEntryAdded;
            LittleLogConsoleDatabase.Instance.EntriesCleared += OnEntriesCleared;
        }

        private void CreateGUI()
        {
            InitializeWindow();

            DrawToolbar();

            DrawEntriesContainer();
        }

        #region Window GUI Creation

        private void DrawToolbar()
        {
            Toolbar toolbar = new Toolbar();

            ToolbarButton clearButton = new ToolbarButton(LittleLogConsoleDatabase.Instance.ClearEntries)
            {
                text = "Clear"
            };
            
            toolbar.Add(clearButton);
            
            rootVisualElement.Add(toolbar);
        }

        private void DrawEntriesContainer()
        {
            _entriesContainer = new ScrollView();
            
            rootVisualElement.Add(_entriesContainer);
            
            DrawAllEntries();
        }
        #endregion
        
        #region React to LittleLogDatabase events

        private void OnEntryAdded(LittleLogEntry entry)
        {
            DrawEntry(entry);
        }

        private void OnEntriesCleared()
        {
            ClearEntries();
        }

        #endregion

        #region Entries

        private void DrawEntry(LittleLogEntry entry)
        {
            LittleLogEditorEntry editorEntry = new LittleLogEditorEntry(entry)
            {
                style =
                {
                    backgroundColor = _isNextEntryEven ? new StyleColor(new Color(0.27f, 0.27f, 0.27f)) : new StyleColor(new Color(0.22f, 0.22f, 0.22f))
                }
            };
            
            
            _entriesContainer.Add(editorEntry);
            _isNextEntryEven = !_isNextEntryEven;
            Debug.Log(_isNextEntryEven);
        }

        private void DrawAllEntries()
        {
            foreach (LittleLogEntry logEntry in LittleLogConsoleDatabase.Instance.LogEntries)
            {
                DrawEntry(logEntry);
            }
        }

        private void ClearEntries()
        {
            _entriesContainer.Clear();
        }
        
        #endregion

        private void OnDisable()
        {
            LittleLogConsoleDatabase.Instance.EntryAdded -= OnEntryAdded;
            LittleLogConsoleDatabase.Instance.EntriesCleared -= OnEntriesCleared;
        }
    }
}
