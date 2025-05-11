using ProjectContent.Code.Csharps;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class SlotView : MonoBehaviour, IPointerClickHandler
  {
    public int index;
    public Image SelectedImage;
    public Image ItemImage;
    public TextMeshProUGUI CountText;
    private bool _isHide;
    private UIController _uiController;
    public InventoryView inventoryView;

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

    public void UpdateSlot(Slot slot)
    {
      if (slot.SlotData.Item == null)
      {
        Hide();
      }
      else
      {
        if (_isHide) 
          Show(slot);
        ItemImage.sprite = slot.SlotData.Item.Icon;
        CountText.text = "" + slot.SlotData.Count;
      }
    }
    
    public void Show(Slot slot)
    {
      
      CountText.gameObject.SetActive(true);
      ItemImage.gameObject.SetActive(true);
      _isHide = false;
    }

    public void Hide()
    {
      CountText.gameObject.SetActive(false);
      ItemImage.gameObject.SetActive(false);
      _isHide = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      switch (eventData.button)
      {
        case PointerEventData.InputButton.Left:
          
          Slot dragSlot = _uiController.DragItems.DraggingSlot;
          Inventory dragInventory = _uiController.DragItems.DraggingInventory;
          Slot selfSlot = inventoryView.Inventory.slots[index];
          if (dragSlot == null || dragSlot.SlotData.Item == null)
          {
            if (selfSlot.SlotData.Item != null)
            {
              _uiController.DragItems.DraggingSlot = selfSlot;
              _uiController.DragItems.DraggingInventory = inventoryView.Inventory;
            }
          }
          else
          {
            if (selfSlot.SlotData.Item != null)
            {
              if (selfSlot.SlotData.Item == dragSlot.SlotData.Item)
              {
                // Добавление предмета в слот
              }
              else
              {
                // Своп с драгбл слотом
                SwapSlots(dragInventory, dragSlot);
              }
            }
            else
            {
              SwapSlots(dragInventory, dragSlot);
            }
          }
          
          break;
        case PointerEventData.InputButton.Right:
          
          break;
      }
    }

    private void SwapSlots(Inventory dragInventory, Slot dragSlot)
    {
      Slot.SwapData(inventoryView.Inventory.slots[index], dragSlot);
      _uiController.DragItems.DraggingSlot = null;
      _uiController.DragItems.DraggingInventory = null;
    }
  }
}