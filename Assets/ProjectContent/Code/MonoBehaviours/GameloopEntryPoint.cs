using System;
using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.Csharps.Architecture.States;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder
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