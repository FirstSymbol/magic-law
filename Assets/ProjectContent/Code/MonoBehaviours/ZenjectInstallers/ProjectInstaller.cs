using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
  public class ProjectInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<GameInput>().FromNew().AsSingle().NonLazy();
    }
  }
}