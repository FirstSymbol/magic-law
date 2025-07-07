using System;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  /// <summary>
  ///   Действие использования отвечающее за передвижение на определенное кол-во единиц относительно текущей позиции.
  /// </summary>
  [Serializable]
  public class MoveBy : UsageAction
  {
    public Vector2 moveOffset;

    public override void Execute(GameObject sender)
    {
      sender.transform.position += new Vector3(moveOffset.x, moveOffset.y, 0f);
    }
  }
}