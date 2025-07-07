using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Скрипт-компонент для взаимодействия с другими объектами
  /// </summary>
  [RequireComponent(typeof(Creature))]
  public class Interaction : MonoBehaviour
  {
    public CollisionObserver Trigger;
    public readonly HashSet<IInteractableEntity> Interactions = new();
    private GameInput _gameInput;

    private void Start()
    {
      Init();
    }

    private void OnDestroy()
    {
      _gameInput.Player.Interaction.performed += Interact;

      Trigger.OnColliderEnter -= EnterInteraction;
      Trigger.OnColliderExit -= ExitInteraction;
      Trigger.OnTriggerEnter -= EnterInteraction;
      Trigger.OnTriggerExit -= ExitInteraction;
    }

    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }


    private void Interact(InputAction.CallbackContext obj)
    {
      var t = GetCurrentInteraction();
      if (t != null)
        t.Interact(gameObject);
    }

    public void EnterInteraction(GameObject interactor)
    {
      if (interactor.TryGetComponent(out IInteractableEntity interactable))
        if (!Interactions.Contains(interactable))
        {
          Interactions.Add(interactable);
          Debug.Log("Added new interactor " + interactor.name);
        }
    }

    public IInteractableEntity GetCurrentInteraction()
    {
      IInteractableEntity currentInteractable = null;
      var currentDistance = 0f;
      foreach (var interaction in Interactions)
      {
        var t = Vector2.Distance(Input.mousePosition,
          Camera.main.WorldToScreenPoint(interaction.gameObject.transform.position));
        if (currentInteractable == null || t < currentDistance)
        {
          currentInteractable = interaction;
          currentDistance = t;
        }
      }

      return currentInteractable;
    }

    public void ExitInteraction(GameObject interactor)
    {
      if (interactor.TryGetComponent(out IInteractableEntity interactable))
        if (Interactions.Contains(interactable))
        {
          Interactions.Remove(interactable);
          Debug.Log("Removed interactor " + interactor.name);
        }
    }

    private void Init()
    {
      _gameInput.Player.Interaction.performed += Interact;

      Trigger.OnColliderEnter += EnterInteraction;
      Trigger.OnColliderExit += ExitInteraction;
      Trigger.OnTriggerEnter += EnterInteraction;
      Trigger.OnTriggerExit += ExitInteraction;
    }
  }
}