using System;
using ProjectContent.Code.ScriptableObjects.Base;

namespace ProjectContent.Code.Csharps
{
  [Serializable]
  public class SlotData
  {
    public Slot Slot { get; private set; }
    public int Count;
    public ItemConfig Item;
    public Action<int> SlotDataChanged;

    public void SetSlot(Slot slot)
    {
      Slot = slot;
    }
    public SlotData(Slot slot)
    {
      Slot = slot;
    }

    public void SubValue(int value)
    {
      Count -= value;
      if (Count <= 0)
      {
        Item = null;
      }
      SlotDataChanged?.Invoke(Slot.index);
    }
  }
}