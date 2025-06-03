using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class CraftView : MonoBehaviour
  {
    public GameObject SlotPrefab;
    public int2 SelectedItem = new(-1, -1);
    public Button craftButton;
    public ItemCoastView ItemCoastView;
    public DefaultSlotView DefaultSlotView;
    private readonly List<CraftSlotView> slots = new();
    public CraftStation CraftStation { get; private set; }

    private void Awake()
    {
      craftButton.onClick.AddListener(OnClickCraftButton);
      DefaultSlotView.Hide();
    }

    private void OnClickCraftButton()
    {
      if (CraftStation == null) return;
      Debug.Log("Clicked Craft Station");
      CraftStation.Craft(SelectedItem.x, SelectedItem.y);
    }

    public void Connect(CraftStation craftStation)
    {
      Disconnect();
      DefaultSlotView.Hide();
      CraftStation = craftStation;

      for (var i = 0; i < craftStation.CraftBundles.Length; i++)
      for (var j = 0; j < craftStation.CraftBundles[i].Items.Count; j++)
      {
        var t = Instantiate(SlotPrefab, transform).GetComponent<CraftSlotView>();
        slots.Add(t);
        t.Select(false);
        t.Connect(this, j, i);
      }

      foreach (var slot in slots) slot.UpdateSlot(CraftStation.CraftBundles[slot.CraftBundleIndex].Items[slot.Index]);
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
      var childrens = new List<GameObject>();

      for (var i = 0; i < transform.childCount; i++)
        childrens.Add(transform.GetChild(i).gameObject);

      foreach (var child in childrens)
        Destroy(child);
    }
  }
}