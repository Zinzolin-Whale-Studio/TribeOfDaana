using System;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSubsystems.Clickable;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapScenePlayerController : MonoBehaviour
    {
        #region Fields
        // Inputs //
        [SerializeField] private InputActionReference m_leftClickInputAction;
        [SerializeField] private InputActionReference m_mouseDeltaInputAction;

        // Drag //
        private bool m_isDragging;
        
        // LeftClick Tap //
        [SerializeField] private LayerMask m_clickableMask;
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

            m_leftClickInputAction.action.performed += OnLeftClickEvent;
            m_leftClickInputAction.action.canceled += OnLeftClickEvent;
            m_leftClickInputAction.action.Enable();
            
            m_mouseDeltaInputAction.action.performed += OnMouseDeltaEvent;
            m_mouseDeltaInputAction.action.Enable();
            
            return true;
        }
        
        private void OnDestroy()
        {
            m_leftClickInputAction.action.performed -= OnLeftClickEvent;
            m_leftClickInputAction.action.canceled -= OnLeftClickEvent;
            m_leftClickInputAction.action.Disable();
            
            m_mouseDeltaInputAction.action.performed -= OnMouseDeltaEvent;
            m_mouseDeltaInputAction.action.Disable();
        }
        #endregion


        #region React To InputAction Events
        
        private void OnLeftClickEvent(InputAction.CallbackContext ctx)
        {
            switch (ctx.interaction)
            {
                case TapInteraction:
                {
                    if (ctx.performed)
                    {
                        //MapSceneDebug.LogWarning("LeftClick Tap Performed");
                        Vector2 mouseWorldPos =
                            Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());

                        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 100f, m_clickableMask);

                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.TryGetComponent(out IClickable clickable))
                            {
                                clickable.Click();
                            }
                            
                        }
                    }

                    break;
                }
                case HoldInteraction:
                {
                    if (ctx.performed)
                    {
                        m_isDragging = true;
                        //MapSceneDebug.LogWarning("LeftClick Hold Performed");
                    }

                    if (ctx.canceled)
                    {
                        m_isDragging = false;
                        //MapSceneDebug.LogWarning("LeftClick Hold Canceled");
                    }

                    break;
                }
            }
        }
        
        private void OnMouseDeltaEvent(InputAction.CallbackContext ctx)
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
