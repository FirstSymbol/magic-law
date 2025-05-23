using System;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TMPro;
using TriInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  public class SlotSelector : MonoBehaviour
  {
    public Inventory Inventory;
    [ReadOnly] public int SelectedSlotIndex = 0;
    public int2 inventoryRange;
    public Creature creature;
    public TextMeshProUGUI SlotText;
    public Action<Slot> SlotSwitched;
    public Action<int,int> SlotSwitchedIndexes;
    
    private GameInput _gameInput;

    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }

    private void OnEnable()
    {
      Inventory.OnSlotUpdated += SlotUpdate;
      _gameInput.Player.ScrollWheel.started += SwitchSlot;
    }

    private void OnDisable()
    {
      Inventory.OnSlotUpdated -= SlotUpdate;
      _gameInput.Player.ScrollWheel.started -= SwitchSlot;
    }

    private void Start()
    {
      UpdateView();
    }

    private void SlotUpdate(int index)
    {
      if (index != SelectedSlotIndex) return;
      
      UpdateView();
      //SlotSwitched?.Invoke(GetSelectedSlot());
      //SlotSwitchedIndexes?.Invoke(SelectedSlotIndex, SelectedSlotIndex);
    }

    public Slot GetSelectedSlot()
    {
      return Inventory.slots[SelectedSlotIndex];
    }

    private void SwitchSlot(InputAction.CallbackContext obj)
    {
      var scroll = obj.ReadValue<float>();
      var oldIndex = SelectedSlotIndex;
      if (scroll < 0)
      {
        SelectedSlotIndex -= 1;
        if (SelectedSlotIndex < inventoryRange.x)
          SelectedSlotIndex = inventoryRange.y;
      }
      else if (scroll > 0)
      {
        SelectedSlotIndex += 1;
        if (SelectedSlotIndex > inventoryRange.y)
          SelectedSlotIndex = inventoryRange.x;
      }
    
      UpdateView();
      SlotSwitchedIndexes?.Invoke(oldIndex, SelectedSlotIndex);
      SlotSwitched?.Invoke(GetSelectedSlot());
    }
    
    // МЕГА КОСТЫЛЬ!!!! ЧИСТО ДЛЯ ДЕБАГА

    private void UpdateView()
    {
      var t = GetSelectedSlot();
      if (SlotText is not null)
        SlotText.text = "Selected slot: " + (t.SlotData.Item == null ? "Null" : t.SlotData.Item.Name);
    }
  }
}