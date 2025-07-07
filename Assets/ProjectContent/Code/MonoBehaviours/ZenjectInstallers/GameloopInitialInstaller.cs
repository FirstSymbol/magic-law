using ProjectContent.Code.Csharps;
using ProjectContent.Code.Csharps.Architecture;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
  public class GameloopInitialInstaller : MonoInstaller
  {
    public GameObject PlayerPrefab;
    private CreatureFabric _creatureFabric;

    private GameloopStateMachine _stateMachine;

    public override void InstallBindings()
    {
      _creatureFabric = new CreatureFabric(PlayerPrefab);
      _stateMachine = new GameloopStateMachine(_creatureFabric);

      Container.Bind<CreatureFabric>().FromInstance(_creatureFabric).AsSingle().NonLazy();
      Container.Bind<GameloopStateMachine>().FromInstance(_stateMachine).AsSingle().NonLazy();
    }
  }
}