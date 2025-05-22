using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Code.ScriptableObjects;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  public class Inventory : MonoBehaviour, IStoreItem
  {
    [field: SerializeField] public Slot[] slots { get; private set; } = new Slot[6];
    public Inventory SubInventory;
    public Action<int> OnSlotUpdated;
    public Action<int> OnSlotSetted;
    public Action<int> OnSlotFill;

    private Dictionary<InvFilterType, List<InvFilterParam>> _addFilter = new()
    {
      { InvFilterType.Exclude , new () { InvFilterParam.MaxCount }},
    };

    private Dictionary<InvFilterType, List<InvFilterParam>> _removeFiler = new()
    {
      {InvFilterType.Exclude, new () { InvFilterParam.Empty }}
    };
    
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
      
      InventoryFilter inventoryFilter = new InventoryFilter(this, item, _addFilter);

      List<int> t = inventoryFilter.Filter();
      
      foreach (int i in t)
      {
        count -= AddItemByIndex(i, item, count);
        if (count <= 0) return;
      }

      if (count > 0)
      {
        if (SubInventory != null)
          SubInventory.AddItem(item, count);
        else
          item.Drop(transform.position, count);
      }
    }

    public void DropItemByIndex(int index, int count)
    {
      SlotData slotData = slots[index].SlotData;
      if (slotData.Item == null) return;
      
      slotData.Item.Drop(transform.position, count);

      slotData.SubValue(count);
    }

    public void RemoveItem(ItemConfig item, int count)
    {
      InventoryFilter inventoryFilter = new(this, item, _removeFiler);
      foreach (int i in inventoryFilter.Filter())
      {
        count -= RemoveItemByIndex(i,item, count);
        if (count <= 0) return;
      }
      
      if (count > 0 && SubInventory != null) 
        SubInventory.RemoveItem(item, count);
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

      if (SubInventory != null)
      {
        count += SubInventory.GetItemCount(item);
      }
      return count;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">index of slot</param>
    /// <param name="item">config of item you want to add</param>
    /// <param name="count"></param>
    /// <returns>Added item count</returns>
    private int AddItemByIndex(int index, ItemConfig item,int count)
    {
      int usedCount = 0;
      
      if (count > item.MaxCount)
        usedCount = item.MaxCount;
      else
        usedCount = count;
      
      if (slots[index].SlotData.Item == null)
      {
        slots[index].SlotData.Item = item;
        slots[index].SlotData.Count = usedCount;
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <param name="count"></param>
    /// <returns>Removed amount of item</returns>
    private int RemoveItemByIndex(int index, ItemConfig item, int count)
    {
      int removedCount = slots[index].SlotData.Count;
      slots[index].SlotData.SubValue(removedCount);
      OnSlotUpdated?.Invoke(index);
      return removedCount;
    }
  }

  public interface IStoreItem
  {
    public Slot[] slots { get; }
  }
}