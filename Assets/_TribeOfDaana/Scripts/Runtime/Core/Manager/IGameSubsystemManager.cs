using UnityEngine.SceneManagement;

namespace _TribeOfDaana.Scripts.Runtime.Core.Manager
{
    public interface IGameSubsystemManager : IManager
    {
        /// <summary>
        /// Function called to initialize game wide manager fields that should be reset at every scene load
        /// </summary>
        public abstract bool SynchronizeWithScene();
        
    }
}
