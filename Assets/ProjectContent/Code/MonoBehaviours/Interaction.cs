using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(Creature))]
  public class Interaction : MonoBehaviour
  {
    public ColissionObserver trigger;
    public readonly HashSet<IInteractableEntity> Interactions = new HashSet<IInteractableEntity>();
    private GameInput _gameInput;

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
      {
        if (!Interactions.Contains(interactable))
        {
          Interactions.Add(interactable);
          Debug.Log("Added new interactor " + interactor.name);
        }
      }
    }

    public IInteractableEntity GetCurrentInteraction()
    {
      
      IInteractableEntity currentInteractable = null;
      float currentDistance = 0f;
      foreach (IInteractableEntity interaction in Interactions)
      {
        var t = Vector2.Distance(Input.mousePosition, Camera.main.WorldToScreenPoint(interaction.gameObject.transform.position));
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
      {
        if (Interactions.Contains(interactable))
        {
          Interactions.Remove(interactable);
          Debug.Log("Removed interactor " + interactor.name);
        }
      }
    }

    private void OnEnable()
    {
      _gameInput.Player.Interaction.performed += Interact;
      
      trigger.OnColliderEnter += EnterInteraction;
      trigger.OnColliderExit += ExitInteraction;
      trigger.OnTriggerEnter += EnterInteraction;
      trigger.OnTriggerExit += ExitInteraction;
    }

    private void OnDisable()
    {
      _gameInput.Player.Interaction.performed += Interact;
      
      trigger.OnColliderEnter -= EnterInteraction;
      trigger.OnColliderExit -= ExitInteraction;
      trigger.OnTriggerEnter -= EnterInteraction;
      trigger.OnTriggerExit -= ExitInteraction;
    }
  }
}