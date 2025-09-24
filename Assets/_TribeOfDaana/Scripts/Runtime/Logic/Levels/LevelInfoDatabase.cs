using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.Levels
{
    [CreateAssetMenu(fileName = "LevelInfoDatabase", menuName = "Scriptable Objects/Tribe Of Daana/Levels/Level Info Database", order = 0)]
    public class LevelInfoDatabase : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<string, LevelInfo> LevelInfos { get; private set; } = new SerializedDictionary<string, LevelInfo>();

        private string _levelToLoadKey = string.Empty;

        public void SetLevelToLoadKey(string levelKey)
        {
            if (string.IsNullOrEmpty(levelKey)) throw new ArgumentException("Can't give a null key");

            _levelToLoadKey = levelKey;
        }
        
        public LevelInfo GetLevelToLoad()
        {
            if (!LevelInfos.ContainsKey(_levelToLoadKey)) throw new Exception("The level key to load registered in the database doesn't exist in the dictionary keys of level infos dictionary.");
            
            return LevelInfos[_levelToLoadKey];
        }
    }
}