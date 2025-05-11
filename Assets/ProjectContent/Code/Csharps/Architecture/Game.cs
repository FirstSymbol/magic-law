using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.MonoBehaviours.Architecture;

namespace ProjectContent.Code.Csharps.Architecture
{
  public class Game
  {
    public GameStateMachine stateMachine;
    public CoroutineRunner CoroutineRunner;

    public Game(CoroutineRunner coroutineRunner)
    {
      CoroutineRunner = coroutineRunner;
      stateMachine = new GameStateMachine(CoroutineRunner);
      stateMachine.Enter<InitialState>();
    }
  }
}