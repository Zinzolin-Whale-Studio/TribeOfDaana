using System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.DontDestroyOnLoad
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
