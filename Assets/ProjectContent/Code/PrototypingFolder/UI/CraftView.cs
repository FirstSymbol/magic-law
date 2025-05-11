using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class CraftView : MonoBehaviour
  {
    public CraftStation CraftStation { get; private set; }
    public GameObject SlotPrefab;
    private List<CraftSlotView> slots = new List<CraftSlotView>();
    public int SelectedItem = -1;
    public Button craftButton;

    private void Awake()
    {
      craftButton.onClick.AddListener(OnClickCraftButton);
    }

    private void OnClickCraftButton()
    {
      if (CraftStation == null) return;
      
      CraftStation.Craft(SelectedItem);
    }

    public void Connect(CraftStation craftStation)
    {
      Disconnect();
      CraftStation = craftStation;
      
      for (int i = 0; i < craftStation.CraftBundle.Items.Count; i++)
      {
        var t = Instantiate(SlotPrefab, transform).GetComponent<CraftSlotView>();
        slots.Add(t);
        t.Select(false);
        t.Connect(this, i);
      }
      
      foreach (var slot in slots)
      {
        slot.UpdateSlot(CraftStation.CraftBundle.Items[slot.index]);
      }
    }

    public void Disconnect()
    {
      if (CraftStation != null)
      {
        ClearChildrens();
        slots.Clear();
        CraftStation = null;
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
  }
}