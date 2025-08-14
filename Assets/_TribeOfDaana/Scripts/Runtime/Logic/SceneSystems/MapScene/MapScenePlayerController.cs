using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.MapScene
{
    public class MapScenePlayerController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private InputActionReference _leftClickInputAction;
        [SerializeField] private InputActionReference _mousePosInputAction;
        #endregion

        #region LifeCycle
        public bool InitializeController()
        {
            return true;
        }

        public bool StartController()
        {
            if (_leftClickInputAction == null || _mousePosInputAction == null)
            {
                MapSceneDebug.LogError("An InputActionReference is null");
                return false;
            }

            _leftClickInputAction.action.started += OnLeftClickEvent;
            _leftClickInputAction.action.performed += OnLeftClickEvent;
            _leftClickInputAction.action.canceled += OnLeftClickEvent;
            _leftClickInputAction.action.Enable();
            
            _mousePosInputAction.action.started += OnMousePosEvent;
            _mousePosInputAction.action.performed += OnMousePosEvent;
            _mousePosInputAction.action.canceled += OnMousePosEvent;
            _mousePosInputAction.action.Enable();
            
            return true;
        }
        
        private void OnDestroy()
        {
            _leftClickInputAction.action.started -= OnLeftClickEvent;
            _leftClickInputAction.action.performed -= OnLeftClickEvent;
            _leftClickInputAction.action.canceled -= OnLeftClickEvent;
            _leftClickInputAction.action.Disable();
            
            _mousePosInputAction.action.started -= OnMousePosEvent;
            _mousePosInputAction.action.performed -= OnMousePosEvent;
            _mousePosInputAction.action.canceled -= OnMousePosEvent;
            _mousePosInputAction.action.Disable();
        }
        #endregion


        #region React To InputAction Events
        
        private void OnLeftClickEvent(InputAction.CallbackContext obj)
        {
        }
        
        private void OnMousePosEvent(InputAction.CallbackContext obj)
        {
        }

        #endregion
    }
}
