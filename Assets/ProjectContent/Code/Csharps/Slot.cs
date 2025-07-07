using System;
using ProjectContent.Code.MonoBehaviours;

namespace ProjectContent.Code.Csharps
{
  /// <summary>
  ///   Класс хранилища предмета(слота) со всеми его настройками.
  /// </summary>
  [Serializable]
  public class Slot
  {
    public int index;
    public Inventory Inventory;
    public Action<int> OnSlotSet;

    /// <summary>
    ///   Slot1 - A Slot; Slot2 - B Slot.
    /// </summary>
    public Action<Slot, Slot> OnSlotSwap;

    public Action<int> OnSlotUpdated;

    public Slot(Inventory inventory, int index)
    {
      SlotData = new SlotData(this);
      SlotData.SlotDataChanged += InvokeOnSlotUpdated;
      this.index = index;
      Inventory = inventory;
      OnSlotUpdated += inventory.InvokeOnSlotUpdated;
      OnSlotSet += inventory.InvokeOnSlotSetted;
    }

    public Slot()
    {
      SlotData = new SlotData(this);
    }

    public SlotData SlotData { get; private set; }

    public void InvokeOnSlotUpdated(int slotIndex)
    {
      OnSlotUpdated?.Invoke(slotIndex);
    }

    /// <summary>
    ///   Установка произвольных данных в слот.
    /// </summary>
    /// <param name="slotData"></param>
    /// <param name="t"></param>
    private void SetData(SlotData slotData, bool t = true)
    {
      SlotData.SlotDataChanged -= InvokeOnSlotUpdated;
      SlotData = slotData;
      SlotData.SetSlot(this);
      SlotData.SlotDataChanged += InvokeOnSlotUpdated;
      OnSlotSet?.Invoke(index);
      OnSlotUpdated?.Invoke(index);
    }

    /// <summary>
    ///   Помять данные слота местами.
    /// </summary>
    /// <param name="slotA"></param>
    /// <param name="slotB"></param>
    public static void SwapData(Slot slotA, Slot slotB)
    {
      var tempSlot = slotA.SlotData;
      slotA.SetData(slotB.SlotData);
      slotB.SetData(tempSlot);
    }
  }
}