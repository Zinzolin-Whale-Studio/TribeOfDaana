using System;
using _TribeOfDaana.Scripts.Runtime.Logic.Clickable;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapSceneSystem
{
    public class MapPlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _mousePosInputAction;
        private Vector2 _screenMousePos;
        
        [SerializeField] private InputActionReference _leftClickInputAction;
        [SerializeField] private LayerMask _clickableLayerMask;

        private void OnEnable()
        {
            _mousePosInputAction.action.performed += OnMousePosChanged;
            _leftClickInputAction.action.canceled += OnLeftClickUp;
        }

        private void OnDisable()
        {
            _mousePosInputAction.action.performed -= OnMousePosChanged;
            _leftClickInputAction.action.canceled -= OnLeftClickUp;
        }

        private void OnMousePosChanged(InputAction.CallbackContext ctx)
        {
            _screenMousePos = ctx.ReadValue<Vector2>();
        }
        
        private void OnLeftClickUp(InputAction.CallbackContext ctx)
        {
            Debug.Log("Click");
            
            Vector2 mouseWorldPos= Camera.current.ScreenToWorldPoint(_screenMousePos);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f, _clickableLayerMask);
            
            if(hit.collider == null) return;

            if (hit.collider.TryGetComponent(out IClickable clickable))
            {
                clickable.Click();
            }
        }
    }
}
