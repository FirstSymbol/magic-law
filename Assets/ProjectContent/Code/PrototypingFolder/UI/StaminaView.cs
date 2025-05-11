using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TMPro;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class StaminaView : MonoBehaviour
  {
    public TextMeshProUGUI staminaText;
    public Creature creature;

    private void Awake()
    {
      creature.creatureStats.Stamina.OnValueChanged += ChangeText;
      ChangeText(creature.creatureStats.Stamina);
    }

    private void ChangeText(StatBase obj)
    {
      staminaText.text = "Stamina: " + obj.Value;
    }

    private void OnDestroy()
    {
      creature.creatureStats.Health.OnValueChanged -= ChangeText;
    }
  }
}