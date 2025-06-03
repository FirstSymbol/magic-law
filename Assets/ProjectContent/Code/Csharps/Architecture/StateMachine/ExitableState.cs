namespace ProjectContent.Code.Csharps.Architecture.StateMachine
{
  public abstract class ExitableState
  {
    protected readonly StateMachineBase StateMachine;

    public ExitableState(StateMachineBase stateMachine)
    {
      StateMachine = stateMachine;
    }

    public abstract void Exit();
  }
}