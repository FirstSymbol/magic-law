using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;

namespace ProjectContent.Code.Csharps.Architecture
{
  public class Game
  {
    public GameStateMachine stateMachine;
    public CoroutineRunner CoroutineRunner;

    public Game(CoroutineRunner coroutineRunner, LoadingScreen ls)
    {
      CoroutineRunner = coroutineRunner;
      stateMachine = new GameStateMachine(CoroutineRunner,ls);
      stateMachine.Enter<InitialState>();
    }
  }
}