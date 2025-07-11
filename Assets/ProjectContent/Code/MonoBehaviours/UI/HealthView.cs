﻿using ProjectContent.Code.Csharps;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class HealthView : MonoBehaviour
  {
    public TextMeshProUGUI HealthText;
    private CreatureFabric _creatureFabric;
    private Player _player;

    private void Start()
    {
      if (_creatureFabric.Player != null)
        OnPlayerCreated();
      _creatureFabric.OnPlayerCreated += OnPlayerCreated;
    }

    private void OnDestroy()
    {
      if (_player != null)
        _player.CreatureStats.Health.OnValueChanged -= ChangeText;
      _creatureFabric.OnPlayerCreated -= OnPlayerCreated;
    }


    [Inject]
    private void Inject(CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
    }

    private void ChangeText(StatBase obj)
    {
      HealthText.text = "Health: " + obj.Value;
    }

    private void OnPlayerCreated()
    {
      _player = _creatureFabric.Player;
      _player.CreatureStats.Health.OnValueChanged += ChangeText;
      ChangeText(_player.CreatureStats.Health);
    }
  }
}