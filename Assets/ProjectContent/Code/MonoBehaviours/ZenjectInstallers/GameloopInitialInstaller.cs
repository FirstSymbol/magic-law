using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.PrototypingFolder;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{ 

  public class GameloopInitialInstaller : MonoInstaller
  {
    public GameObject PlayerPrefab;
    
    private GameloopStateMachine _stateMachine;
    private CreatureFabric _creatureFabric;

    public override void InstallBindings()
    {
      _creatureFabric = new CreatureFabric(PlayerPrefab);
      _stateMachine = new GameloopStateMachine(_creatureFabric);
            
      Container.Bind<CreatureFabric>().FromInstance(_creatureFabric).AsSingle().NonLazy();
      Container.Bind<GameloopStateMachine>().FromInstance(_stateMachine).AsSingle().NonLazy();
    }
  }
}