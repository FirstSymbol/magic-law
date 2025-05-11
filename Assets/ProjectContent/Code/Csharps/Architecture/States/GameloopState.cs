using ProjectContent.Code.Csharps.Architecture.StateMachine;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class GameloopState : DefaultState
  {
    public GameloopState(StateMachineBase stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
      Debug.Log("[GameloopState] Enter");
    }

    public override void Exit()
    {
      Debug.Log("[GameloopState] Exit");
    }
  }
}