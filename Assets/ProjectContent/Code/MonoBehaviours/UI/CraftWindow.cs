using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class CraftWindow : UIWindow
  {
    private GameInput _gameInput;
    public CraftView View;
    private Player _player;
    
    [Inject]
    private void Inject(GameInput gameInput, Player player)
    {
      _gameInput = gameInput;
      _player = player;
    }
    public override void Initialize()
    {
      _gameInput.UI.OpenCraft.performed += ToggleOpen;
      gameObject.SetActive(false);
      View.Connect(_player.PlayerCraftingStation);
    }

    private void ToggleOpen(InputAction.CallbackContext obj)
    {
      Toggle();
    }

    public override void Toggle()
    {
      if (IsOpened)
      {
        _windowsController.CloseWindow(this);
      }
      else
      {
        _windowsController.OpenWindow<CraftWindow>();
      } 
    }

    public override void Close()
    {
      base.Close();
      View.Connect(_player.PlayerCraftingStation);
    }

    private void OnDestroy()
    {
      _gameInput.UI.OpenCraft.performed -= ToggleOpen;
    }
  }
}