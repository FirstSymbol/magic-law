using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  /// <summary>
  /// Базовый класс входных точек
  /// </summary>
  [RequireComponent(typeof(EntryPoint))]
  public abstract class EntryPointBase : MonoBehaviour
  {
    public virtual void AwakeEnter(){}
    public virtual void StartEnter(){}
  }
}