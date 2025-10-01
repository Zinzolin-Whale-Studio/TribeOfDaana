using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _TribeOfDaana.Scripts.Runtime.Core.Manager
{
    public abstract class SceneSystemManager<T> : Manager<SceneSystemManager<T>>, ISceneSystemManager
    {
        public abstract bool StartScene();
    }
}
