using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.Csharps.Enums;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class InitialGameloopState : DefaultState
  {
    public InitialGameloopState(StateMachineBase stateMachine) : base(stateMachine)
    {
    }

    public override void Exit()
    {
      
    }

    public override void Enter()
    {
        StateMachine.Enter<LoadSceneState, string>(ConstValues.Scenes.GameloopScene);
        Exit();
    }
  }
}