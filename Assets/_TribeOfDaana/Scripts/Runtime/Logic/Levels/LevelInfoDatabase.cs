using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.Levels
{
    [CreateAssetMenu(fileName = "LevelInfoDatabase", menuName = "Scriptable Objects/Tribe Of Daana/Levels/Level Info Database", order = 0)]
    public class LevelInfoDatabase : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<string, LevelInfo> LevelInfos { get; private set; } = new SerializedDictionary<string, LevelInfo>();
    }
}