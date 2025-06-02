using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder
{
  [RequireComponent(typeof(EntryPoint))]
  public abstract class EntryPointBase : MonoBehaviour
  {
    public virtual void AwakeEnter(){}
    public virtual void StartEnter(){}
  }
}