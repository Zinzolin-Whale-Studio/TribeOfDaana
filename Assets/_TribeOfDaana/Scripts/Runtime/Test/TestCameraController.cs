using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.Test
{
    public class TestCameraController : MonoBehaviour
    {
        [SerializeField] private InputActionReference m_leftClickInputAction;
        [SerializeField] private InputActionReference m_mouseDeltaInputAction;

        private bool m_isDragging;
        
        private void OnEnable()
        {
            m_leftClickInputAction.action.started += OnLeftClickEvent;
            m_leftClickInputAction.action.canceled += OnLeftClickEvent;
            m_mouseDeltaInputAction.action.performed += OnMouseDeltaEvent;
        }

        private void OnDisable()
        {
            m_leftClickInputAction.action.started -= OnLeftClickEvent;
            m_leftClickInputAction.action.canceled -= OnLeftClickEvent;
            m_mouseDeltaInputAction.action.performed -= OnMouseDeltaEvent;
        }

        private void Update()
        {
            
        }

        private void OnLeftClickEvent(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                m_isDragging = true;
            }

            if (ctx.canceled)
            {
                m_isDragging = false;
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
    }
}
