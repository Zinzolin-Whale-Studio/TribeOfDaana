using System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.Singleton
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T _instance;
        
        protected virtual void Awake()
        {
            InitializeSingleton(false);
        }

        protected void InitializeSingleton(bool isPersistent)
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
                if(isPersistent) DontDestroyOnLoad(gameObject);
            }
        } 
    }

    public abstract class PersistentMonoBehaviourSingleton<T> : MonoBehaviourSingleton<T>
        where T : PersistentMonoBehaviourSingleton<T>
    {
        protected override void Awake()
        {
            InitializeSingleton(true);
        }
    }
}
