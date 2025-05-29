using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class StaminaView : MonoBehaviour
  {
    public TextMeshProUGUI StaminaText;
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

    private void Init()
    {
      _player = _creatureFabric.Player;
      _player.creatureStats.Stamina.OnValueChanged += ChangeText;
      ChangeText(_player.creatureStats.Stamina);
    }
    private void ChangeText(StatBase obj)
    {
      StaminaText.text = "Stamina: " + obj.Value;
    }

    private void OnDestroy()
    {
      if (_player != null)
      {
        _player.creatureStats.Stamina.OnValueChanged -= ChangeText;
      }
      _creatureFabric.OnPlayerCreated -= Init;
    }
  }
}