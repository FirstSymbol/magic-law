using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
  public class ProjectInstaller : MonoInstaller
  {
    public LoadingScreen LoadingScreen;
    public CoroutineRunner CoroutineRunner;

    public override void InstallBindings()
    {
      Game.Clear();

      Container.Bind<GameInput>().FromNew().AsSingle().NonLazy();
      Container.Bind<LoadingScreen>().FromInstance(LoadingScreen).AsSingle().NonLazy();
      Container.Bind<CoroutineRunner>().FromInstance(CoroutineRunner).AsSingle().NonLazy();
    }
  }
}