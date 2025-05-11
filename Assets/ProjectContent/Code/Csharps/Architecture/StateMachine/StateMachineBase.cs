using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Architecture.StateMachine
{
  public abstract class StateMachineBase
  {
    protected ExitableState ActiveState { get; set; }
    protected Dictionary<Type, ExitableState> _states { get; set; }
    
    public StateMachineBase(){}

    public void Enter<TState>() where TState : DefaultState
    {
      ChangeState<TState>();
    }
    public void Enter<TState, TPayload>(TPayload payload) where TState : PayloadState<TPayload>
    {
      ChangeState<TState, TPayload>(payload);
    }

    private void ChangeState<TState>() where TState : DefaultState
    {
      if (ExitAndSwitch<TState>()) return;
      ((DefaultState)ActiveState).Enter();
    }

    private void ChangeState<TState, TPayload>(TPayload payload) where TState : PayloadState<TPayload>
    {
      if (ExitAndSwitch<TState>()) return;
      ((PayloadState<TPayload>)ActiveState).Enter(payload);
    }

    private bool ExitAndSwitch<TState>() where TState : ExitableState
    {
      try
      {
        ActiveState?.Exit();
        ActiveState = _states[typeof(TState)];
        return false;
      }
      catch (KeyNotFoundException e)
      {
        Debug.LogError($"[StateMachine] State not found: {typeof(TState).Name}]");
        return false;
      }
    }
  }
}