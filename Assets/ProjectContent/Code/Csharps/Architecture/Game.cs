using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using Zenject;

namespace ProjectContent.Code.Csharps.Architecture
{
  public static class Game
  {
    public static GameStateMachine StateMachine;
    public static CoroutineRunner CoroutineRunner;

    public static void Clear()
    {
      StateMachine = null;
      CoroutineRunner = null;
    }
    public static void Initialize(CoroutineRunner coroutineRunner, LoadingScreen ls)
    {
      Clear();
      CoroutineRunner = coroutineRunner;
      StateMachine = new GameStateMachine(CoroutineRunner,ls);
      StateMachine.Enter<InitialState>();
    }
  }
}