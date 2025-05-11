using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.MonoBehaviours.Architecture;

namespace ProjectContent.Code.Csharps.Architecture
{
  public class GameStateMachine: StateMachineBase
  {
    public GameStateMachine(CoroutineRunner coroutineRunner)
    {
      _states = new Dictionary<Type, ExitableState>()
      {
        {typeof(InitialState), new InitialState(this)},
        {typeof(LoadSceneState),new LoadSceneState(this,coroutineRunner)},
        {typeof(GameloopState), new GameloopState(this)},
      };
    }
  }
}