using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectContent.Code.Csharps.Item.Usages
{
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