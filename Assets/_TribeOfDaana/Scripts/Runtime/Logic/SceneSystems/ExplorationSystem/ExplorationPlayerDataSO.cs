using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    [CreateAssetMenu(fileName = "ExplorationPlayerDataSO", menuName = "ScriptableObjects/ExplorationSystem/ExplorationPlayerDataSO")]
    public class ExplorationPlayerDataSO : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed { get; private set; } = 10;
    }
}
