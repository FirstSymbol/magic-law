using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Code.ScriptableObjects;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  /// Компонент инвентаря для хранения предметов в слотах.
  /// </summary>
  public class Inventory : MonoBehaviour, IStoreItem
  {
    [field: SerializeField] public Slot[] Slots { get; private set; } = new Slot[6];
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
      for (int i = 0; i < Slots.Length; i++)
      {
        Slots[i] = new Slot(this, i);
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
    
    /// <summary>
    /// Добавить определенное количество предметов в инвентарь.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
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
    /// <summary>
    /// Выкинуть определенное количество предметов из слота по индексу.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    public void DropItemByIndex(int index, int count)
    {
      SlotData slotData = Slots[index].SlotData;
      if (slotData.Item == null) return;
      
      slotData.Item.Drop(transform.position, count);

      slotData.SubValue(count);
    }
    
    /// <summary>
    ///  Удаление определенного количества предметов по конфигу
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void RemoveItem(ItemConfig item, int count)
    {
      InventoryFilter inventoryFilter = new(this, item, _removeFiler);
      foreach (int i in inventoryFilter.Filter())
      {
        count -= RemoveItemByIndex(i);
        if (count <= 0) return;
      }
      
      if (count > 0 && SubInventory != null) 
        SubInventory.RemoveItem(item, count);
    }
    /// <summary>
    /// Получение количества предмета по конфигу в инвентаре, а также в связных инвентарях.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetItemCount(ItemConfig item)
    {
      int count = 0;
      foreach (Slot slot in Slots)
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
    /// Добавить предмет в слот по индексу
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
      
      if (Slots[index].SlotData.Item == null)
      {
        Slots[index].SlotData.Item = item;
        Slots[index].SlotData.Count = usedCount;
        OnSlotFill?.Invoke(index);
        OnSlotUpdated?.Invoke(index);
      }
      else if (Slots[index].SlotData.Item == item && Slots[index].SlotData.Count < item.MaxCount)
      {
          
        if (Slots[index].SlotData.Count + count > item.MaxCount)
        {
          Slots[index].SlotData.Count = item.MaxCount;
          usedCount = item.MaxCount - Slots[index].SlotData.Count;
        }
        else
        {
          Slots[index].SlotData.Count += count;
          usedCount = count;
        }
        OnSlotUpdated?.Invoke(index);
      }
      
      return usedCount;
    }
    
    /// <summary>
    /// Удалить предмет из слота по индексу
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Removed amount of item</returns>
    private int RemoveItemByIndex(int index)
    {
      int removedCount = Slots[index].SlotData.Count;
      Slots[index].SlotData.SubValue(removedCount);
      OnSlotUpdated?.Invoke(index);
      return removedCount;
    }
  }
}