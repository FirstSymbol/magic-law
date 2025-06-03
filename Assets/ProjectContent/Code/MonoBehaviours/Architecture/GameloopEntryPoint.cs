using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.Csharps.Architecture.States;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  public class GameloopEntryPoint : EntryPointBase
  {
    private GameloopStateMachine _stateMachine;

    [Inject]
    private void Inject(GameloopStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public override void StartEnter()
    {
      _stateMachine.Enter<InitialGameloopState>();
    }
  }
}