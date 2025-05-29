using ProjectContent.Code.Csharps.Architecture.StateMachine;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class InitialGameloopState : DefaultState
  {
    private DiContainer _sceneContainer;
    public InitialGameloopState(StateMachineBase stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
      Debug.Log("[InitialGameloopState] Enter");
      StateMachine.Enter<SpawnCreaturesState>();
    }

    public override void Exit()
    {
      Debug.Log("[InitialGameloopState] Exit");
    }
  }
}