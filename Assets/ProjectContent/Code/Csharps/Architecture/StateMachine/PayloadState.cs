namespace ProjectContent.Code.Csharps.Architecture.StateMachine
{
  public abstract class PayloadState<TPayload> : ExitableState
  {
    public PayloadState(StateMachineBase stateMachine) : base(stateMachine)
    {
    }

    public abstract void Enter(TPayload payload);

  }
  
}   