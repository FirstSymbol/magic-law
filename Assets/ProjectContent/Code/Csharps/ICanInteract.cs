using ProjectContent.Code.MonoBehaviours;
using UnityEngine;

namespace ProjectContent.Code.Csharps
{
  public interface ICanInteract
  {
    public Interaction Interaction { get; }
    public void Interact(GameObject target = null);
  }
}