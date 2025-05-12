using _TribeOfDaana.Scripts.Runtime.Core.StateMachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _TribeOfDaana.Scripts.Runtime.Logic.SceneSystems.ExplorationSystem
{
    public class ExplorationController : StateMachineController
    {
        #region Fields
        [SerializeField] private InputActionReference _walkInputAction;

        public UnityAction WalkStarted;
        public UnityAction WalkEnded;
        #endregion
        
        public override void InitializeController()
        {
            
        }
        
        private void OnEnable()
        {
            _walkInputAction.action.started += OnWalkActionTriggered;
            _walkInputAction.action.performed += OnWalkActionTriggered;
            _walkInputAction.action.canceled += OnWalkActionTriggered;
        }

        /// <summary>
        /// React to any change of state in the <see cref="_walk"/> action
        /// </summary>
        /// <param name="ctx">Callback context</param>
        private void OnWalkActionTriggered(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                WalkStarted?.Invoke();
            }

            if (ctx.performed)
            {
                
            }

            if (ctx.canceled)
            {
                WalkEnded?.Invoke();
            }
        }

        private void OnDisable()
        {
            _walkInputAction.action.started -= OnWalkActionTriggered;
            _walkInputAction.action.performed -= OnWalkActionTriggered;
            _walkInputAction.action.canceled -= OnWalkActionTriggered;
        }
    }
}
