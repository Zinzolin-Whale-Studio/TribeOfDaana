using System;
using _TribeOfDaana.Scripts.Runtime.Logic.SceneSubsystems.MovementSubsystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private InputActionReference _walkInputAction;

        [SerializeField] private Walk _walk;
        #endregion

        private void OnEnable()
        {
            _walkInputAction.action.started += OnWalkActionTriggered;
            _walkInputAction.action.performed += OnWalkActionTriggered;
            _walkInputAction.action.canceled += OnWalkActionTriggered;
        }

        /// <summary>
        /// React to any change of state in the <see cref="_walk"/> action
        /// </summary>
        /// <param name="obj">Callback context</param>
        private void OnWalkActionTriggered(InputAction.CallbackContext ctx)
        {
            
        }

        private void OnDisable()
        {
            _walkInputAction.action.started -= OnWalkActionTriggered;
            _walkInputAction.action.performed -= OnWalkActionTriggered;
            _walkInputAction.action.canceled -= OnWalkActionTriggered;
        }
    }
}
