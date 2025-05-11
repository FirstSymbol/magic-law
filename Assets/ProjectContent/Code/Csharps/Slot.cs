using System;
using UnityEngine.Rendering.Universal;

namespace ProjectContent.Code.Csharps
{
  [Serializable]
  public class Slot
  {
    public int index;
    public SlotData SlotData { get; private set; }
    public Action<int> OnSlotUpdated;
    public Action<int> OnSlotSet;

    /// <summary>
    ///   Slot1 - A Slot; Slot2 - B Slot.
    /// </summary>
    public Action<Slot,Slot> OnSlotSwap;
    public Inventory Inventory;
    
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
    
    public void InvokeOnSlotUpdated(int slotIndex)
    {
      OnSlotUpdated?.Invoke(slotIndex);
    }

    private void SetData(SlotData slotData, bool t = true)
    {
      SlotData.SlotDataChanged -= InvokeOnSlotUpdated;
      SlotData = slotData;
      SlotData.SetSlot(this);
      SlotData.SlotDataChanged += InvokeOnSlotUpdated;
      OnSlotSet?.Invoke(index);
      OnSlotUpdated?.Invoke(index);
    }

    public static void SwapData(Slot slotA, Slot slotB)
    {
      SlotData tempSlot = slotA.SlotData;
      slotA.SetData(slotB.SlotData);
      slotB.SetData(tempSlot);
    }
  }
}