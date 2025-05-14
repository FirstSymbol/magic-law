using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TMPro;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class StaminaView : MonoBehaviour
  {
    public TextMeshProUGUI StaminaText;
    public Creature Creature;

    private void Awake()
    {
      Creature.creatureStats.Stamina.OnValueChanged += ChangeText;
      ChangeText(Creature.creatureStats.Stamina);
    }

    private void ChangeText(StatBase obj)
    {
      StaminaText.text = "Stamina: " + obj.Value;
    }

    private void OnDestroy()
    {
      Creature.creatureStats.Health.OnValueChanged -= ChangeText;
    }
  }
}