using UnityEngine;

namespace ProjectContent.Code.Csharps
{
  /// <summary>
  /// Интерфейс для взаимодействия с сущностью(собой)
  /// </summary>
  public interface IInteractableEntity
  {
    public bool IsInteracting { get; set; }
    public GameObject InteractorObject { get; set; }
    GameObject gameObject { get; }
    public void Interact(GameObject sender);
  }
}