using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.Csharps.Enums;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class InitialState : DefaultState
  {
    public InitialState(StateMachineBase stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
      Debug.Log("[InitialState] Enter");
      
      StateMachine.Enter<LoadSceneState, string>(ConstValues.Scenes.MainMenu);
    }

    public override void Exit()
    {
      Debug.Log("[InitialState] Exit");
    }
  }
}