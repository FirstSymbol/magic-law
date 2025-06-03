using System;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  /// <summary>
  ///   Действие использования отвечающее за передвижение в определенную позицию.
  /// </summary>
  [Serializable]
  public class MoveTo : UsageAction
  {
    public Vector3 WorldPoint;

    public override void Execute(GameObject Target)
    {
      Target.transform.position = WorldPoint;
    }
  }
}