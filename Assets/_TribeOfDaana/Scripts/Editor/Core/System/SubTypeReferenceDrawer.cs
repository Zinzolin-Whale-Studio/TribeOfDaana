using System;
using System.Collections.Generic;
using System.Reflection;
using _TribeOfDaana.Scripts.Runtime.Core.System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace _TribeOfDaana.Scripts.Editor.Core.System
{
    [CustomPropertyDrawer(typeof(SubTypeReference<>))]
    public class SubTypeReferenceDrawer : PropertyDrawer
    {
        private bool _isDrawerInitialized;
        
        private List<Type> _cachedTypes = new List<Type>();
        private string[] _cachedTypeNames;
        private int _selectedTypeIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_isDrawerInitialized)
            {
                InitDrawer(property);
            }
            
            EditorGUI.BeginProperty(position, label, property);
            _selectedTypeIndex = EditorGUI.Popup(new Rect(position.x, position.y, position.width, position.height), _selectedTypeIndex, _cachedTypeNames);
            EditorGUI.EndProperty();
        }

        private void InitDrawer(SerializedProperty property)
        {
            _isDrawerInitialized = true;
            
            Type targetType = property.serializedObject.targetObject.GetType();

            string[] parts = property.propertyPath.Split('.');
            
            string listFieldName = parts[0];

            FieldInfo listFieldInfo = null;
            
            while ( listFieldInfo == null)
            {
                listFieldInfo  = targetType.GetField(listFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (listFieldInfo == null)
                {
                    targetType = targetType.BaseType;
                    if(targetType == null ) break;
                }
            }
            
            Type subTypeReferenceType = listFieldInfo.FieldType.GenericTypeArguments[0];
            Type parentType = subTypeReferenceType.GenericTypeArguments[0];

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            _cachedTypes.Clear();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(parentType))
                    {
                        _cachedTypes.Add(type);
                    }
                }
            }

            _cachedTypeNames = new string[_cachedTypes.Count];

            for (int i = 0; i < _cachedTypes.Count; i++)
            {
                _cachedTypeNames[i] = _cachedTypes[i].Name;
            }
        }
    }
}