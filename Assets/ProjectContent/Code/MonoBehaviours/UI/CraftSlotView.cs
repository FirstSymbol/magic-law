using ProjectContent.Code.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class CraftSlotView : SlotView
  {
    public CraftView CraftStationView;
    public int CraftBundleIndex { get; private set; }

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }

    public void Connect(CraftView craftStationView, int index, int craftBundleIndex)
    {
      Index = index;
      CraftStationView = craftStationView;
      CraftBundleIndex = craftBundleIndex;
    }
    
    new public void UpdateSlot(ItemConfig item)
    {
      if (item == null)
      {
        Hide();
      }
      else
      {
        if (_isHide) 
          Show();
        ItemImage.sprite = item.Icon;
        CountText.text = "" + item.CraftCount;
      }
    }
    
    public override void Show()
    {
      _isHide = true;
      gameObject.SetActive(true);
    }

    public override void Hide()
    {
      _isHide = false;
      gameObject.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      CraftStationView.SelectedItem = new int2(CraftBundleIndex, Index);
    }
  }
}