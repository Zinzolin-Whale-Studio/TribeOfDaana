using System;
using _TribeOfDaana.Scripts.Runtime.Core.Singleton;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.Manager
{
    public abstract class Manager<T> : MonoBehaviourSingleton<T>, IManager where T : Manager<T>
    {
        public abstract bool InitializeManager();
    }
}
