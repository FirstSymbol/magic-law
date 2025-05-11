using System;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Item.Usages
{
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