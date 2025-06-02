using System;
using ProjectContent.Code.ScriptableObjects;

namespace ProjectContent.Code.Csharps
{
  /// <summary>
  /// Класс хранилище конфига предмета и его количества.
  /// </summary>
  [Serializable]
  public class CraftElement
  {
    public ItemConfig Item;
    public int Amount;
  }
}