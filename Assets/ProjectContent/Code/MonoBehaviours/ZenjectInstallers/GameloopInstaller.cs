using ProjectContent.Code.MonoBehaviours.UI;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.ZenjectInstallers
{
    public class GameloopInstaller : MonoInstaller
    {
        public MovementController movementController;
        public WindowsController windowsController;
        public UIController uiController;
        public Player Player;
        public override void InstallBindings()
        {
            Container.Bind<MovementController>().FromInstance(movementController).AsSingle().NonLazy();
            Container.Bind<WindowsController>().FromInstance(windowsController).AsSingle().NonLazy();
            Container.Bind<UIController>().FromInstance(uiController).AsSingle().NonLazy();
            Container.Bind<Player>().FromInstance(Player).AsSingle().NonLazy();
        }//
    }
}