using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class InventoryView : MonoBehaviour
  {
    public Inventory Inventory { get; private set; }
    public SlotSelector selector;
    public GameObject SlotPrefab;
    private List<SlotView> slots = new List<SlotView>();
    public void Connect(Inventory inventory)
    {
      Disconnect();
      Inventory = inventory;
      inventory.OnSlotUpdated += UpdateSlot;
      
      for (int i = 0; i < inventory.slots.Length; i++)
      {
        var t = Instantiate(SlotPrefab, transform).GetComponent<SlotView>();
        slots.Add(t);
        t.Select(false);
        t.index = i;
        t.inventoryView = this;
      }
      
      foreach (var slot in slots)
      {
        slot.UpdateSlot(inventory.slots[slot.index]);
      }
    }

    public void Disconnect()
    {
      if (Inventory != null)
      {
        ClearChildrens();
        slots.Clear();
        Inventory.OnSlotUpdated -= UpdateSlot;
        Inventory = null;
      }
    }

    private void ClearChildrens()
    {
      List<GameObject> childrens = new List<GameObject>();
      
      for (int i = 0; i < transform.childCount; i++) 
        childrens.Add(transform.GetChild(i).gameObject);

      foreach (GameObject child in childrens) 
        Destroy(child);
    }

    private void UpdateSlot(int index)
    {
      slots[index].UpdateSlot(Inventory.slots[index]);
    }

    private void OnDestroy()
    {
      if(Inventory != null) Inventory.OnSlotUpdated -= UpdateSlot;
    }
  }
}