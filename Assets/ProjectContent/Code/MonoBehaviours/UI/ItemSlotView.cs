﻿using ProjectContent.Code.Csharps;
using UnityEngine.EventSystems;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class ItemSlotView : SlotView
  {
    public InventoryView InventoryView;

    public void Connect(InventoryView inventoryView, int index)
    {
      Index = index;
      InventoryView = inventoryView;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      switch (eventData.button)
      {
        case PointerEventData.InputButton.Left:

          var dragSlot = _uiController.DragItems.DraggingSlot;
          var dragInventory = _uiController.DragItems.DraggingInventory;
          var selfSlot = InventoryView.Inventory.Slots[Index];
          if (dragSlot == null || dragSlot.SlotData.Item == null)
          {
            if (selfSlot.SlotData.Item != null)
            {
              _uiController.DragItems.DraggingSlot = selfSlot;
              _uiController.DragItems.DraggingInventory = InventoryView.Inventory;
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
                SwapSlots(dragSlot);
              }
            }
            else
            {
              SwapSlots(dragSlot);
            }
          }

          break;
        case PointerEventData.InputButton.Right:

          break;
      }
    }

    private void SwapSlots(Slot dragSlot)
    {
      Slot.SwapData(InventoryView.Inventory.Slots[Index], dragSlot);
      _uiController.DragItems.DraggingSlot = null;
      _uiController.DragItems.DraggingInventory = null;
    }
  }
}