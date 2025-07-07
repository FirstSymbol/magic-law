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
    [ReadOnly] public int SelectedSlotIndex;
    [FormerlySerializedAs("inventoryRange")] public int2 InventoryRange;
    [FormerlySerializedAs("creature")] public Creature Creature;
    public TextMeshProUGUI SlotText;

    private GameInput _gameInput;
    public Action<Slot> SlotSwitched;
    public Action<int, int> SlotSwitchedIndexes;

    private void Start()
    {
      Init();
      UpdateView();
    }

    private void OnDestroy()
    {
      Inventory.OnSlotUpdated -= SlotUpdate;
      _gameInput.Player.ScrollWheel.started -= SwitchSlot;
    }

    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }

    private void Init()
    {
      Inventory.OnSlotUpdated += SlotUpdate;
      _gameInput.Player.ScrollWheel.started += SwitchSlot;
    }

    private void SlotUpdate(int index)
    {
      if (index != SelectedSlotIndex) return;

      UpdateView();
    }

    public Slot GetSelectedSlot()
    {
      return Inventory.Slots[SelectedSlotIndex];
    }

    private void SwitchSlot(InputAction.CallbackContext obj)
    {
      var scroll = obj.ReadValue<float>();
      var oldIndex = SelectedSlotIndex;
      if (scroll < 0)
      {
        SelectedSlotIndex -= 1;
        if (SelectedSlotIndex < InventoryRange.x)
          SelectedSlotIndex = InventoryRange.y;
      }
      else if (scroll > 0)
      {
        SelectedSlotIndex += 1;
        if (SelectedSlotIndex > InventoryRange.y)
          SelectedSlotIndex = InventoryRange.x;
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