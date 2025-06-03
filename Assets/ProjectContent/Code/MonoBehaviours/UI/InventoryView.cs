using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class InventoryView : MonoBehaviour
  {
    public Inventory Inventory { get; private set; }
    public GameObject SlotPrefab;
    private List<ItemSlotView> slots = new List<ItemSlotView>();
    public void Connect(Inventory inventory)
    {
      Disconnect();
      Inventory = inventory;
      inventory.OnSlotUpdated += UpdateSlot;
      
      for (int i = 0; i < inventory.Slots.Length; i++)
      {
        var t = Instantiate(SlotPrefab, transform).GetComponent<ItemSlotView>();
        slots.Add(t);
        t.Select(false);
        t.Connect(this, i);
      }
      
      foreach (var slot in slots)
      {
        slot.UpdateSlot(inventory.Slots[slot.Index]);
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
      slots[index].UpdateSlot(Inventory.Slots[index]);
    }

    private void OnDestroy()
    {
      if(Inventory != null) Inventory.OnSlotUpdated -= UpdateSlot;
    }
  }
}