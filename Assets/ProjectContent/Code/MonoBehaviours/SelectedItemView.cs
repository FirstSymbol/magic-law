﻿using ProjectContent.Code.Csharps;
using TMPro;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  public class SelectedItemView : MonoBehaviour
  {
    public TextMeshProUGUI SlotText;
    private CreatureFabric _creatureFabric;
    private SlotSelector _slotSelector;

    private void Start()
    {
      if (_creatureFabric.Player != null)
        OnPlayerCreated();
      _creatureFabric.OnPlayerCreated += OnPlayerCreated;
    }

    private void OnDestroy()
    {
      _creatureFabric.OnPlayerCreated -= OnPlayerCreated;
      if (_slotSelector is not null)
        _slotSelector.SlotSwitched -= UpdateView;
    }

    [Inject]
    private void Inject(CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
    }

    private void OnPlayerCreated()
    {
      _slotSelector = _creatureFabric.Player.SlotSelector;

      _slotSelector.SlotSwitched += UpdateView;
      UpdateView(_slotSelector.GetSelectedSlot());
    }

    private void UpdateView(Slot t)
    {
      if (SlotText is not null)
        SlotText.text = "Selected slot: " + (t.SlotData.Item == null ? "Null" : t.SlotData.Item.Name);
    }
  }
}