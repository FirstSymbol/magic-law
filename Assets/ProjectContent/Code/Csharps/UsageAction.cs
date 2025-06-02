using System;
using UnityEngine;

namespace ProjectContent.Code.Csharps
{
  /// <summary>
  /// Клас действия для предмета.
  /// </summary>
  [Serializable]
  public abstract class UsageAction
  {
    public abstract void Execute(GameObject Target);
  }
}