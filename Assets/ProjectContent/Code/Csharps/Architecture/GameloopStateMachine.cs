using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using ProjectContent.Code.PrototypingFolder;
using Zenject;

namespace ProjectContent.Code.Csharps.Architecture
{
  public class GameloopStateMachine : StateMachineBase
  {
    public GameloopStateMachine(CreatureFabric creatureFabric)
    { 
      _states = new Dictionary<Type, ExitableState>()
      {
        {typeof(InitialGameloopState),new InitialGameloopState(this)},
        {typeof(SpawnCreaturesState),new SpawnCreaturesState(this, creatureFabric)},
        {typeof(GameloopState), new GameloopState(this)}
      };
    }
  }
}