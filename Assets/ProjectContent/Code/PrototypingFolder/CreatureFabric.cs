using System;
using ProjectContent.Code.MonoBehaviours.Creatures;
using Zenject;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Enums;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;


namespace ProjectContent.Code.PrototypingFolder
{
  public class CreatureFabric
  {
    private GameObject _playerPrefab;
    public Player Player;
    public Action OnPlayerCreated;
    
    public CreatureFabric(GameObject playerPrefab)
    {
      _playerPrefab = playerPrefab;
    }

    public Player CreatePlayer(Transform transform)
    {
      Player = Object.Instantiate(_playerPrefab, transform.position, Quaternion.identity).GetComponent<Player>();
      Player.transform.SetParent(transform.parent);
      OnPlayerCreated?.Invoke();
      return Player;
    }
  }
}