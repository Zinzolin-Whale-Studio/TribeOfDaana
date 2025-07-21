using System;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Runtime.Core.System
{
    [Serializable]
    public class TypeReference
    {
        [SerializeField] private string _typeName;

        public Type Type => Type.GetType(_typeName);

        public void SetType(Type type)
        {
            _typeName = type.AssemblyQualifiedName;
        }
    }
}
