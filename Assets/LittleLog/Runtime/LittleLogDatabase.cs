using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using UnityEditor;
using UnityEngine;

namespace LittleLog.Runtime
{
    public class LittleLogDatabase : ScriptableObject
    {
        #region Fields
        private List<LittleLogEntry> _logEntries = new List<LittleLogEntry>();
        #endregion
        
        #region Actions

        public event Action<LittleLogEntry> EntryAdded;
        #endregion
        
        #region Singleton Instance
        private const string AssetName = nameof(LittleLogDatabase) + ".asset";
        
        private static LittleLogDatabase _instance;

        public static LittleLogDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<LittleLogDatabase>(nameof(LittleLogDatabase));
                    
                    if (_instance == null)
                    {
                        _instance = CreateInstance<LittleLogDatabase>();

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

        public void AddLogEntry(string text)
        {
            LittleLogEntry entry = new LittleLogEntry(text);
            _logEntries.Add(entry);
            EntryAdded?.Invoke(entry);
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
