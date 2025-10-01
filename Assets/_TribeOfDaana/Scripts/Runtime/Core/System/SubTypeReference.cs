using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _TribeOfDaana.Scripts.Runtime.Core.System
{
    [Serializable]
    public class SubTypeReference<TParentType>
    {
        [SerializeField] private string _typeName;
        
        public Type Type => Type.GetType(_typeName);

        public void SetType(Type type)
        {
            _typeName = type.AssemblyQualifiedName;
        }
    }
}