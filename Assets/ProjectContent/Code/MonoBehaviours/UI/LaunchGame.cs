using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.Csharps.Architecture.States;
using ProjectContent.Code.Csharps.Enums;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class LaunchGame : MonoBehaviour
  {

    public void Launch()
    {
      Game.StateMachine.Enter<LoadSceneState, string>(ConstValues.Scenes.GameloopScene);
    }
    
  }
}