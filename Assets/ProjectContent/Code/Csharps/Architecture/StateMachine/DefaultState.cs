namespace ProjectContent.Code.Csharps.Architecture.StateMachine
{
  public abstract class DefaultState : ExitableState 
  {
    protected DefaultState(StateMachineBase stateMachine) : base(stateMachine)
    {
    }

    public abstract void Enter();
  }
}