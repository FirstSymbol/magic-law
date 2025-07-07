using System;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using UnityEngine;
using Object = UnityEngine.Object;


namespace ProjectContent.Code.Csharps
{
  /// <summary>
  ///   Фабрика создания существ
  /// </summary>
  public class CreatureFabric
  {
    private readonly GameObject _playerPrefab;
    public Action OnPlayerCreated;
    public Player Player;

    public CreatureFabric(GameObject playerPrefab)
    {
      _playerPrefab = playerPrefab;
    }

    /// <summary>
    ///   Создает игрока
    /// </summary>
    /// <param name="transform"></param>
    /// <returns>Player</returns>
    public Player CreatePlayer(Transform transform)
    {
      Player = Object.Instantiate(_playerPrefab, transform.position, Quaternion.identity).GetComponent<Player>();
      Player.transform.SetParent(transform.parent);
      OnPlayerCreated?.Invoke();
      Debug.LogWarning("[CreatureFabric] Player created!");
      return Player;
    }
  }
}