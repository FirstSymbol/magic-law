using System;
using ProjectContent.Code.ScriptableObjects;

namespace ProjectContent.Code.Csharps
{
  /// <summary>
  ///   Класс данных который используется в слоте и содержит данные о хранимом предмете, его количестве, а также методы для
  ///   взаимодействия с предметом.
  /// </summary>
  [Serializable]
  public class SlotData
  {
    public int Count;
    public ItemConfig Item;
    public Action<int> SlotDataChanged;

    public SlotData(Slot slot)
    {
      Slot = slot;
    }

    public Slot Slot { get; private set; }

    public void SetSlot(Slot slot)
    {
      Slot = slot;
    }

    public void SubValue(int value)
    {
      if (Item == null) return;
      Count -= value;
      if (Count <= 0) Item = null;
      SlotDataChanged?.Invoke(Slot.index);
    }

    public void AddValue(int value)
    {
      if (Item == null) return;
      Count += value;
      if (Count >= Item.MaxCount) return;
      SlotDataChanged?.Invoke(Slot.index);
    }
  }
}