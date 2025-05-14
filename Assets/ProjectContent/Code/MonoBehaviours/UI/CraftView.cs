using System.Collections.Generic;
using ProjectContent.Code.PrototypingFolder;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class CraftView : MonoBehaviour
  {
    public CraftStation CraftStation { get; private set; }
    public GameObject SlotPrefab;
    private List<CraftSlotView> slots = new List<CraftSlotView>();
    public int2 SelectedItem = new int2(-1, -1);
    public Button craftButton;

    private void Awake()
    {
      craftButton.onClick.AddListener(OnClickCraftButton);
    }

    private void OnClickCraftButton()
    {
      if (CraftStation == null) return;
      
      CraftStation.Craft(SelectedItem.x, SelectedItem.y);
    }

    public void Connect(CraftStation craftStation)
    {
      Disconnect();
      CraftStation = craftStation;

      for (int i = 0; i < craftStation.CraftBundles.Length; i++)
      {
        for (int j = 0; j < craftStation.CraftBundles[i].Items.Count; j++)
        {
          var t = Instantiate(SlotPrefab, transform).GetComponent<CraftSlotView>();
          slots.Add(t);
          t.Select(false);
          t.Connect(this, j, i);
        }
      }
      
      foreach (CraftSlotView slot in slots)
      {
        slot.UpdateSlot(CraftStation.CraftBundles[slot.CraftBundleIndex].Items[slot.Index]);
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