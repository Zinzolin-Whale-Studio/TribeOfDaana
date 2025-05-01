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
        
        private Dictionary<string, int> _propertiesDictionary = new Dictionary<string, int>();
        private List<Type> _cachedTypes = new List<Type>();
        private string[] _cachedTypeNames;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_isDrawerInitialized)
            {
                if(!InitDrawer(property)) return;
            }

            SerializedProperty typeNameProperty = property.FindPropertyRelative("_typeName");
            
            if (_propertiesDictionary.TryAdd(property.propertyPath, -1))
            {
                if (string.IsNullOrEmpty(typeNameProperty.stringValue) || string.IsNullOrWhiteSpace(typeNameProperty.stringValue))
                {
                    _propertiesDictionary[property.propertyPath] = 0;
                    typeNameProperty.stringValue =
                        _cachedTypes[_propertiesDictionary[property.propertyPath]].AssemblyQualifiedName;
                }
                else
                {
                    Type type = Type.GetType(typeNameProperty.stringValue);
                    _propertiesDictionary[property.propertyPath] = _cachedTypes.IndexOf(type);
                }
            }
            
            EditorGUI.BeginProperty(position, label, property);

            int oldIndex = _propertiesDictionary[property.propertyPath];
            
            _propertiesDictionary[property.propertyPath] = 
                EditorGUI.Popup(
                new Rect(position.x, position.y, position.width, position.height), 
                _propertiesDictionary[property.propertyPath],
                _cachedTypeNames);

            if (oldIndex != _propertiesDictionary[property.propertyPath])
            {
                typeNameProperty.stringValue =
                    _cachedTypes[_propertiesDictionary[property.propertyPath]].AssemblyQualifiedName;
                
            }
            EditorGUI.EndProperty();
            
            property.serializedObject.ApplyModifiedProperties();
        }

        private bool InitDrawer(SerializedProperty property)
        {
            _isDrawerInitialized = true;
            
            Type targetType = property.serializedObject.targetObject.GetType();

            string[] parts = property.propertyPath.Split('.');
            
            string fieldName = parts[0];

            FieldInfo fieldInfo = null;
            
            while ( fieldInfo == null)
            {
                fieldInfo  = targetType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (fieldInfo == null)
                {
                    targetType = targetType.BaseType;
                    if(targetType == null ) break;
                }
            }

            if (fieldInfo == null) return false;

            Type subTypeReferenceType = null;
            
            if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>)) // Rn only works well for List<T>
            {
                subTypeReferenceType = fieldInfo.FieldType.GenericTypeArguments[0];
            }
            else
            {
                subTypeReferenceType = fieldInfo.FieldType;
            }
            
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
            
            return true;
        }
    }
}