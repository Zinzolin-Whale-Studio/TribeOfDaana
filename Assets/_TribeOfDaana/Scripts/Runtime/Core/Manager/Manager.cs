using _TribeOfDaana.Scripts.Runtime.Core.Singleton;

namespace _TribeOfDaana.Scripts.Runtime.Core.Manager
{
    public abstract class Manager<T> : MonoBehaviourSingleton<Manager<T>>, IManager
    {
        public abstract bool InitializeManager();
    }
}
