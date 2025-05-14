using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TMPro;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class HealthView : MonoBehaviour
  {
    public TextMeshProUGUI HealthText;
    public Creature Creature;

    private void Awake()
    {
      Creature.creatureStats.Health.OnValueChanged += ChangeText;
      ChangeText(Creature.creatureStats.Health);
    }

    private void ChangeText(StatBase obj)
    {
      HealthText.text = "Health: " + obj.Value;
    }

    private void OnDestroy()
    {
      Creature.creatureStats.Health.OnValueChanged -= ChangeText;
    }
  }
}