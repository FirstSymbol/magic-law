using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class HealthView : MonoBehaviour
  {
    public TextMeshProUGUI HealthText;
    public Creature Creature;
    
    [Inject]
    private void Inject(Player player)
    {
      Creature = player;
    }
    
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