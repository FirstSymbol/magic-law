using ProjectContent.Code.Csharps;
using ProjectContent.Code.PrototypingFolder.Good;
using ProjectContent.Code.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class CraftSlotView : MonoBehaviour, IPointerClickHandler
  {
    public int index;
    public Image SelectedImage;
    public Image ItemImage;
    public TextMeshProUGUI CountText;
    private UIController _uiController;
    public CraftView CraftStationView;

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }

    public void Select(bool selected)
    {
      if (selected)
      {
        SelectedImage.gameObject.SetActive(true);
      }
      else
      {
        SelectedImage.gameObject.SetActive(false);
      }
    }

    public void Connect(CraftView craftStationView, int index)
    {
      this.index = index;
      CraftStationView = craftStationView;
    }

    public void UpdateSlot(ItemConfig item)
    {
      if (item == null)
      {
        Hide();
      }
      else
      {
        if (!gameObject.activeSelf) 
          Show();
        ItemImage.sprite = item.Icon;
        CountText.text = "" + item.CraftCount;
      }
    }
    
    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      CraftStationView.SelectedItem = index;
    }
  }
}