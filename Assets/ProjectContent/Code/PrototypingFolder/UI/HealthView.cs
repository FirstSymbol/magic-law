using System;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TMPro;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class HealthView : MonoBehaviour
  {
    public TextMeshProUGUI healthText;
    public Creature creature;

    private void Awake()
    {
      creature.creatureStats.Health.OnValueChanged += ChangeText;
      ChangeText(creature.creatureStats.Health);
    }

    private void ChangeText(StatBase obj)
    {
      healthText.text = "Health: " + obj.Value;
    }

    private void OnDestroy()
    {
      creature.creatureStats.Health.OnValueChanged -= ChangeText;
    }
  }
}