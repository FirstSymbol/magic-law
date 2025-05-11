using System;
using UnityEngine.InputSystem;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace ProjectContent.Code.MonoBehaviours
{
  public class MovementController : UnityEngine.MonoBehaviour
  {
    public Vector2 Velocity;
    
    public InputActionReference moveAction;
    private GameInput _gameInput;

    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }
    private void Awake()
    {
      _gameInput.Player.Move.performed += VectorChange;
      _gameInput.Player.Move.started += VectorChange;
      _gameInput.Player.Move.canceled += VectorChange;
    }
    private void VectorChange(InputAction.CallbackContext obj)
    {
      Velocity = obj.ReadValue<Vector2>();
    }
  }
}