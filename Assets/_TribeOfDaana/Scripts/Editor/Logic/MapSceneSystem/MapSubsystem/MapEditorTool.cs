using _TribeOfDaana.Scripts.Editor.Core.Utilities;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem;
using _TribeOfDaana.Scripts.Runtime.Logic.MapSceneSystem.MapSubsystem.Point;
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

                    switch (evt.keyCode)
                    {
                        case KeyCode.C:
                            CreateMapPoint(evt.mousePosition, sceneView);
                            
                            evt.Use();
                            break;
                        
                        case KeyCode.E:
                           DestroyMapPoint(evt.mousePosition);
                            
                            evt.Use();
                            break;
                        
                        case KeyCode.R:
                            break;
                    }
                    break;
            }
        }

        bool CreateMapPoint(Vector2 eventMousePosition, SceneView sceneView)
        {
            GameObject mapPointGO =
                PrefabUtility.InstantiatePrefab(m_mapPointPrefab, m_targetMap.transform) as GameObject;
                            
            if (mapPointGO == null || !mapPointGO.TryGetComponent(out MapPoint mapPoint))
            {
                Debug.LogWarning("[Map Editor Tool] : Failed to create map point");
                DestroyImmediate(mapPointGO);
                return false;
            }
            
            mapPointGO.transform.position = EditorToolUtils.EventPositionToWorldPosition(eventMousePosition, sceneView);
            Undo.RegisterCreatedObjectUndo(mapPointGO, "Create Map Point");
            
            return true;
        }

        bool DestroyMapPoint(Vector2 eventMousePosition)
        {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(eventMousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldRay.origin, worldRay.direction, 100.0f);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out MapPoint mapPoint))
            {
                Undo.DestroyObjectImmediate(mapPoint.gameObject);
                return true;
            }

            return false;
        }
    }
}
