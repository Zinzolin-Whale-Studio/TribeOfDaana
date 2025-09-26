using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Editor.Logic.MapSceneSystem.MapSubsystem
{
    [EditorTool("Map Editor Tool", typeof(Map))]
    public class MapEditorTool : EditorTool
    {
        [SerializeField] private Texture2D m_toolIconTexture;
        private GUIContent m_toolIconContent;

        private Map m_targetMap;
        [SerializeField] private GameObject m_mapPointPrefab;
        
        public override GUIContent toolbarIcon => m_toolIconContent;

        private void OnEnable()
        {
            m_targetMap = target as Map;

            if (m_targetMap == null)
            {
                Debug.LogWarning("[Map Editor Tool] : Couldn't get the target when loading the tool.");
                return;
            }
            
            if (m_toolIconTexture == null)
            {
                Debug.LogWarning("[Map Editor Tool] : Couldn't load the tool icon.");
                return;
            }

            m_toolIconContent = new GUIContent()
            {
                image = m_toolIconTexture,
                text = "Map Editor Tool",
                tooltip = "Edit map points and connections."
            };
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if(window is not SceneView sceneView || target == null) return;

            Event evt = Event.current;

            switch (evt.type)
            {
                case EventType.KeyDown:

                    if (evt.keyCode == KeyCode.C)
                    {
                        
                        Debug.Log("[Map Editor Tool] : Pressed C");

                        GameObject mapPointGO =
                            PrefabUtility.InstantiatePrefab(m_mapPointPrefab, m_targetMap.transform) as GameObject;
                        
                        if (mapPointGO == null)
                        {
                            Debug.LogWarning("[Map Editor Tool] : Failed to create map point");
                            evt.Use();
                            break;
                        }
                    }
                    
                    if (evt.keyCode == KeyCode.E)
                    {
                        Debug.Log("[Map Editor Tool] : Pressed E");
                    }
                    
                    evt.Use();
                    break;
            }
        }
    }
}
