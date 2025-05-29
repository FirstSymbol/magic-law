using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Code.PrototypingFolder;
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
    
    
    [Inject]
    private void Inject(CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
    }
    private void Start()
    {
      _creatureFabric.OnPlayerCreated += Init;
    }

    private void ChangeText(StatBase obj)
    {
      HealthText.text = "Health: " + obj.Value;
    }

    private void Init()
    {
      _player = _creatureFabric.Player;
      _player.creatureStats.Health.OnValueChanged += ChangeText;
      ChangeText(_player.creatureStats.Health);
    }
    private void OnDestroy()
    {
      if (_player != null)
      {
        _player.creatureStats.Health.OnValueChanged -= ChangeText;
      }
      _creatureFabric.OnPlayerCreated -= Init;
    }
  }
}