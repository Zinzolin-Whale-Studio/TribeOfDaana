using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapScenePlayerController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private InputActionReference m_leftClickInputAction;
        [SerializeField] private InputActionReference m_mouseDeltaInputAction;

        private bool m_isDragging;
        #endregion

        #region LifeCycle
        public bool InitializeController()
        {
            return true;
        }

        public bool StartController()
        {
            if (m_leftClickInputAction == null || m_mouseDeltaInputAction == null)
            {
                MapSceneDebug.LogError("An InputActionReference is null");
                return false;
            }

            m_leftClickInputAction.action.started += OnLeftClickEvent;
            m_leftClickInputAction.action.canceled += OnLeftClickEvent;
            m_leftClickInputAction.action.Enable();
            
            m_mouseDeltaInputAction.action.performed += OnMousePosEvent;
            m_mouseDeltaInputAction.action.Enable();
            
            return true;
        }
        
        private void OnDestroy()
        {
            m_leftClickInputAction.action.started -= OnLeftClickEvent;
            m_leftClickInputAction.action.canceled -= OnLeftClickEvent;
            m_leftClickInputAction.action.Disable();
            
            m_mouseDeltaInputAction.action.performed -= OnMousePosEvent;
            m_mouseDeltaInputAction.action.Disable();
        }
        #endregion


        #region React To InputAction Events
        
        private void OnLeftClickEvent(InputAction.CallbackContext ctx)
        {
            if (ctx.interaction is TapInteraction)
            {
                if (ctx.started)
                {
                    MapSceneDebug.LogWarning("LeftClick Tap Started");
                }

                if (ctx.canceled)
                {
                    MapSceneDebug.LogWarning("LeftClick Tap Canceled");
                }
            }
            else if (ctx.interaction is HoldInteraction)
            {
                if (ctx.started)
                {
                    m_isDragging = true;
                    MapSceneDebug.LogWarning("LeftClick Hold Started");
                }

                if (ctx.canceled)
                {
                    m_isDragging = false;
                    MapSceneDebug.LogWarning("LeftClick Hold Canceled");
                }
            }
        }
        
        private void OnMousePosEvent(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && m_isDragging)
            {
                Vector2 delta = ctx.ReadValue<Vector2>();
                Camera.main.transform.position += (Vector3)(-delta) * Time.deltaTime;
            }
        }

        #endregion
    }
}
