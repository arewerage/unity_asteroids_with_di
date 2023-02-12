using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService, IInitializable, GameInput.IGameplayActions, GameInput.IUIActions
    {
        public event Action<Vector2> Moved;
        public event Action Fired;
        public event Action Played;
        
        private GameInput _gameInput;
        
        public void Initialize()
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
        }

        public void EnableGameplay()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
        }

        public void EnableUI()
        {
            _gameInput.UI.Enable();
            _gameInput.Gameplay.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context) =>
            Moved?.Invoke(context.ReadValue<Vector2>());

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started)
                return;
                
            Fired?.Invoke();
            
            Debug.Log("OnFire");
        }
        
        public void OnPlay(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started)
                return;

            Played?.Invoke();
            // EnableGameplay();
            
            Debug.Log("OnPlay");
        }
    }
}