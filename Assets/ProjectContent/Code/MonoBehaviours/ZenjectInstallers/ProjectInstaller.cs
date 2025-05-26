using ProjectContent.Code.MonoBehaviours.UI;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
  public class ProjectInstaller : MonoInstaller
  {
    public LoadingScreen LoadingScreen;
    public override void InstallBindings()
    {
      Container.Bind<GameInput>().FromNew().AsSingle().NonLazy();
      Container.Bind<LoadingScreen>().FromInstance(LoadingScreen).AsSingle().NonLazy();
    }
  }
}