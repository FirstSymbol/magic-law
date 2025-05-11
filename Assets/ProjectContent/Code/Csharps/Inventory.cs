using System;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Code.ScriptableObjects.Base;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.Csharps
{
  public class Inventory : MonoBehaviour
  {
    public Slot[] slots = new Slot[6];
    public Action<int> OnSlotUpdated;
    public Action<int> OnSlotSetted;
    public Action<int> OnSlotFill;

    private void Awake()
    {
      for (int i = 0; i < slots.Length; i++)
      {
        slots[i] = new Slot(this, i);
      }
    }
    
    public void InvokeOnSlotUpdated(int slotIndex)
    {
      OnSlotUpdated?.Invoke(slotIndex);
    }

    public void InvokeOnSlotSetted(int slotIndex)
    {
      OnSlotSetted?.Invoke(slotIndex);
    }
    
    [Button("Debug AddItem")]
    public void AddItem(ItemConfig item, int count)
    {
      for (int i = 0; i < slots.Length && count > 0; i++) 
        count -= AddItemByIndex(i, item, count);
      
      if (count != 0) Debug.LogWarning($"There is no free space in the inventory - remains items count: {count}");
    }

    public void DropItemByIndex(int index)
    {
      
    }

    public void RemoveItem(ItemConfig item, int count)
    {
      
    }

    public int GetItemCount(ItemConfig item)
    {
      int count = 0;
      foreach (Slot slot in slots)
      {
        if (slot.SlotData.Item == item)
        {
          count += slot.SlotData.Count;
        }
      }
      
      return count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">index of slot</param>
    /// <param name="item">config of item you want to add</param>
    /// <param name="count"></param>
    /// <returns>Remains item count</returns>
    private int AddItemByIndex(int index, ItemConfig item,int count)
    {
      int usedCount = 0;
      if (slots[index].SlotData.Item == null)
      {
        slots[index].SlotData.Item = item;
        slots[index].SlotData.Count = count;
        usedCount = count;
        OnSlotFill?.Invoke(index);
        OnSlotUpdated?.Invoke(index);
      }
      else if (slots[index].SlotData.Item == item && slots[index].SlotData.Count < item.MaxCount)
      {
          
        if (slots[index].SlotData.Count + count > item.MaxCount)
        {
          slots[index].SlotData.Count = item.MaxCount;
          usedCount = item.MaxCount - slots[index].SlotData.Count;
        }
        else
        {
          slots[index].SlotData.Count += count;
          usedCount = count;
        }
        OnSlotUpdated?.Invoke(index);
      }
      
      return usedCount;
    }
    
    private void RemoveItemByIndex(int index, ItemConfig item, int count)
    {
      
    }
  }
}