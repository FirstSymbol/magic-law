using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.Csharps.Architecture.States;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class LaunchGame : MonoBehaviour
  {
    [Inject] private Game _game;

    public void Launch()
    {
      _game.stateMachine.Enter<InitialGameloopState>();
    }
    
  }
}