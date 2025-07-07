using ProjectContent.Code.Csharps;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class SlotView : MonoBehaviour, IPointerClickHandler
  {
    public Image SelectedImage;
    public Image ItemImage;
    public TextMeshProUGUI CountText;
    public int Index;
    public bool IsSelected;
    protected bool _isHide;
    protected UIController _uiController;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }

    public void UpdateSlot(Slot slot)
    {
      if (slot.SlotData.Item == null)
      {
        Hide();
      }
      else
      {
        if (_isHide)
          Show();
        ItemImage.sprite = slot.SlotData.Item.Icon;
        CountText.text = "" + slot.SlotData.Count;
      }
    }

    public virtual void Show()
    {
      _isHide = false;
      CountText.gameObject.SetActive(true);
      ItemImage.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
      _isHide = true;
      CountText.gameObject.SetActive(false);
      ItemImage.gameObject.SetActive(false);
    }

    public void Select(bool selected)
    {
      if (selected)
      {
        IsSelected = true;
        SelectedImage.gameObject.SetActive(true);
      }
      else
      {
        IsSelected = false;
        SelectedImage.gameObject.SetActive(false);
      }
    }
  }
}