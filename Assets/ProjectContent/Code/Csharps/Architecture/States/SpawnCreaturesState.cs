using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class SpawnCreaturesState : DefaultState
  {
    private DiContainer _sceneContainer;
    private CreatureFabric _creatureFabric;
    public SpawnCreaturesState(StateMachineBase stateMachine, CreatureFabric creatureFabric) : base(stateMachine)
    {
      _creatureFabric = creatureFabric;
    }

    public override void Enter()
    {
      Debug.Log("[SpawnCreaturesState] Enter]");
      SpawnPlayer();
      StateMachine.Enter<GameloopState>();
    }

    public override void Exit()
    {
      Debug.Log("[SpawnCreaturesState] Exit]");
    }

    private void SpawnPlayer()
    {
      var p = _creatureFabric.CreatePlayer(GameObject.FindWithTag("PlayerSpawnpoint").transform);
      Debug.LogWarning("[SpawnCreaturesState] Player is spawned");
    }
  }
}