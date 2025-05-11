using ProjectContent.Code.PrototypingFolder.Good;
using ProjectContent.Code.PrototypingFolder.UI;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
    public class GameloopInstaller : MonoInstaller
    {
        public MovementController movementController;
        public WindowsController windowsController;
        public UIController uiController;
        public override void InstallBindings()
        {
            Container.Bind<MovementController>().FromInstance(movementController).AsSingle().NonLazy();
            Container.Bind<WindowsController>().FromInstance(windowsController).AsSingle().NonLazy();
            Container.Bind<UIController>().FromInstance(uiController).AsSingle().NonLazy();
        }//
    }
}