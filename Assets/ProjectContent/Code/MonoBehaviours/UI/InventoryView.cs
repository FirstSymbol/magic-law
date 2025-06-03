using System.Collections.Generic;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class InventoryView : MonoBehaviour
  {
    public GameObject SlotPrefab;
    private readonly List<ItemSlotView> slots = new();
    public Inventory Inventory { get; private set; }

    private void OnDestroy()
    {
      if (Inventory != null) Inventory.OnSlotUpdated -= UpdateSlot;
    }

    public void Connect(Inventory inventory)
    {
      Disconnect();
      Inventory = inventory;
      inventory.OnSlotUpdated += UpdateSlot;

      for (var i = 0; i < inventory.Slots.Length; i++)
      {
        var t = Instantiate(SlotPrefab, transform).GetComponent<ItemSlotView>();
        slots.Add(t);
        t.Select(false);
        t.Connect(this, i);
      }

      foreach (var slot in slots) slot.UpdateSlot(inventory.Slots[slot.Index]);
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
      var childrens = new List<GameObject>();

      for (var i = 0; i < transform.childCount; i++)
        childrens.Add(transform.GetChild(i).gameObject);

      foreach (var child in childrens)
        Destroy(child);
    }

    private void UpdateSlot(int index)
    {
      slots[index].UpdateSlot(Inventory.Slots[index]);
    }
  }
}