using ProjectContent.Code.Csharps.Architecture.StateMachine;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class SpawnCreaturesState : DefaultState
  {
    private readonly CreatureFabric _creatureFabric;
    private DiContainer _sceneContainer;

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
    }
  }
}