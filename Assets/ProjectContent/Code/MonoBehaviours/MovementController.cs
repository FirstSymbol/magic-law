using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Скрипт задания вектора движения.
  /// </summary>
  public class MovementController : MonoBehaviour
  {
    public Vector2 Velocity;
    private GameInput _gameInput;

    private void Awake()
    {
      _gameInput.Player.Move.performed += VectorChange;
      _gameInput.Player.Move.started += VectorChange;
      _gameInput.Player.Move.canceled += VectorChange;
    }

    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }

    private void VectorChange(InputAction.CallbackContext obj)
    {
      Velocity = obj.ReadValue<Vector2>();
    }
  }
}