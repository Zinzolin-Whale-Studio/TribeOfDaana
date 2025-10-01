using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace LittleLog.Runtime
{
    public class LittleLogConsoleDatabase : ScriptableObject
    {
        #region Properties
        public List<LittleLogEntry> LogEntries { get; private set; } = new List<LittleLogEntry>();
        #endregion
        
        #region Actions
        public event Action<LittleLogEntry> EntryAdded;
        public event Action EntriesCleared;
        #endregion
        
        #region Singleton Instance
        private const string AssetName = nameof(LittleLogConsoleDatabase) + ".asset";
        
        private static LittleLogConsoleDatabase _instance;

        public static LittleLogConsoleDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<LittleLogConsoleDatabase>(nameof(LittleLogConsoleDatabase));
                    
                    if (_instance == null)
                    {
                        _instance = CreateInstance<LittleLogConsoleDatabase>();

                        string instancePath = Path.Combine(GetPackageResourcesFolderPath(), AssetName);
                        
                        AssetDatabase.CreateAsset(_instance, instancePath);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }

                return _instance;
            }
        }

        private static string GetPackageResourcesFolderPath()
        {
            string[] directories = Directory.GetDirectories("Assets", "LittleLog", SearchOption.AllDirectories);

            if (directories.Length > 0)
            {
                string packageDirectoryPath = directories[0]; // Return the first found path (you could refine this if you have multiple matches)

                string resourcesDirectoryPath = Path.Combine(packageDirectoryPath, "Resources");
                if (!Directory.Exists(resourcesDirectoryPath))
                {
                    Directory.CreateDirectory(resourcesDirectoryPath);
                }

                return resourcesDirectoryPath;
            }

            return "Assets";
        }
        #endregion

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            CompilationPipeline.compilationStarted += OnCompilationStarted;
        }

        #region Entries

        public void AddLogEntry(LittleLogEntry entry)
        {
            LogEntries.Add(entry);
            EntryAdded?.Invoke(entry);
        }

        public void ClearEntries()
        {
            LogEntries.Clear();
            EntriesCleared?.Invoke();
        }

        #endregion
        
        #region React to UnityEditor events

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    ClearEntries();
                    break;
            }
        }

        private void OnCompilationStarted(object obj)
        {
            ClearEntries();
        }

        #endregion


        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            CompilationPipeline.compilationStarted -= OnCompilationStarted;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}
