using System;
using UnityEngine;

namespace ProjectContent.Code.Csharps
{
  [Serializable]
  public abstract class UsageAction
  {
    public abstract void Execute(GameObject Target);
  }
}