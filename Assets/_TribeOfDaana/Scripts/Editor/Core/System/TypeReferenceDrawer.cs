using UnityEditor;
using _TribeOfDaana.Scripts.Runtime.Core.System;
using UnityEngine.UIElements;

namespace _TribeOfDaana.Scripts.Editor.Core.System
{
    [CustomPropertyDrawer(typeof(TypeReference))]
    public class TypeReferenceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return base.CreatePropertyGUI(property);
        }
    }
}
