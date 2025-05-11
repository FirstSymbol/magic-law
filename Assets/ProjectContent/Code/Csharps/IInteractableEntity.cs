using UnityEngine;

namespace ProjectContent.Code.Csharps
{
  public interface IInteractableEntity
  {
    GameObject gameObject { get; }
    public void Interact(GameObject sender);
  }
}