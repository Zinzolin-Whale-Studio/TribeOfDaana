using UnityEditor;
using UnityEngine;

namespace _TribeOfDaana.Scripts.Editor.Core.Utilities
{
    public static class EditorToolUtils 
    {
        
        public static Vector3 EventPositionToWorldPosition(Vector2 mouseEventPosition, SceneView sceneView)
        {
            Vector2 mouseScreenPosition = HandleUtility.GUIPointToScreenPixelCoordinate(mouseEventPosition);
            float zDistance = 0.0f - sceneView.camera.transform.position.z;
            
            //This is mouse world position
            return sceneView.camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, zDistance));
        }
    }
}
