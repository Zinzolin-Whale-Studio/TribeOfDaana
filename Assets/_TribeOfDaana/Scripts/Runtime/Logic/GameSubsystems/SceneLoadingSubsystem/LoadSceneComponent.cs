using System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Logic.GameSubsystems.SceneLoadingSubsystem
{
    public class LoadSceneComponent : MonoBehaviour
    {
        public event Action<string> LoadOfSceneAsked;
        
        public void AskToLoadScene(string sceneName)
        {
            LoadOfSceneAsked?.Invoke(sceneName);
        }
    }
}
