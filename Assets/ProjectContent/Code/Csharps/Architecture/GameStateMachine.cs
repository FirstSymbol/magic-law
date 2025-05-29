using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;

namespace ProjectContent.Code.Csharps.Architecture
{
  public class GameStateMachine: StateMachineBase
  {
    public GameStateMachine(CoroutineRunner coroutineRunner,LoadingScreen ls)
    {
      _states = new Dictionary<Type, ExitableState>()
      {
        {typeof(InitialState), new InitialState(this)},
        {typeof(LoadSceneState),new LoadSceneState(this,coroutineRunner,ls)}
      };
    }
  }
}