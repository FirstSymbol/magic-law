﻿using System.Collections.Generic;
using ProjectContent.Code.ScriptableObjects;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class ItemCoastView : MonoBehaviour
  {
    public ItemConfig Item;
    public List<DefaultSlotView> slots = new();
    public GameObject SlotPrefab;

    public void Connect(ItemConfig item)
    {
      Disconnect();
      if (item == null) return;
      Item = item;


      for (var i = 0; i < Item.craftCoats.Count; i++)
      {
        var t = Instantiate(SlotPrefab, transform).GetComponent<DefaultSlotView>();
        slots.Add(t);
        t.Select(false);
        t.Connect(i);
      }


      foreach (var slot in slots) slot.UpdateSlot(Item.craftCoats[slot.Index]);
    }

    public void Disconnect()
    {
      if (Item != null)
      {
        ClearChildrens();
        slots.Clear();
        Item = null;
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
  }
}