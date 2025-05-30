using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using ProjectContent.Code.PrototypingFolder;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
    public class GameloopSceneInstaller : MonoInstaller
    {
        public MovementController MovementController;
        public WindowsController WindowsController;
        public UIController UIController;

        private CreatureFabric _creatureFabric;
        private GameloopStateMachine _stateMachine;

        public override void InstallBindings()
        {
            Container.Bind<MovementController>().FromInstance(MovementController).AsSingle().NonLazy();
            Container.Bind<WindowsController>().FromInstance(WindowsController).AsSingle().NonLazy();
            Container.Bind<UIController>().FromInstance(UIController).AsSingle().NonLazy();
        }
    }
}