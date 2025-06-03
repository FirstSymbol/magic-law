using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.Csharps.Architecture.States;

namespace ProjectContent.Code.Csharps.Architecture
{
  public class GameloopStateMachine : StateMachineBase
  {
    public GameloopStateMachine(CreatureFabric creatureFabric)
    {
      _states = new Dictionary<Type, ExitableState>
      {
        { typeof(InitialGameloopState), new InitialGameloopState(this) },
        { typeof(SpawnCreaturesState), new SpawnCreaturesState(this, creatureFabric) },
        { typeof(GameloopState), new GameloopState(this) }
      };
    }
  }
}