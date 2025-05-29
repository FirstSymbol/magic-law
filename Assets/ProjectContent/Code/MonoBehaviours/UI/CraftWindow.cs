using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class CraftWindow : UIWindow
  {
    private GameInput _gameInput;
    public CraftView View;
    private CreatureFabric _creatureFabric;
    private Player _player;
    
    [Inject]
    private void Inject(GameInput gameInput, CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
      _gameInput = gameInput;
    }
    public override void Initialize()
    {
      _creatureFabric.OnPlayerCreated += Init;
      _gameInput.UI.OpenCraft.performed += ToggleOpen;
      gameObject.SetActive(false);
      
    }

    private void Init()
    {
      _player = _creatureFabric.Player;
      View.Connect(_player.PlayerCraftingStation);
    }

    private void ToggleOpen(InputAction.CallbackContext obj)
    {
      Toggle();
    }

    public override void Toggle()
    {
      if (IsOpened)
        _windowsController.CloseWindow(this);
      else
        _windowsController.OpenWindow<CraftWindow>();
    }

    public override void Close()
    {
      base.Close();
      View.Connect(_player.PlayerCraftingStation);
    }

    private void OnDestroy()
    {
      _gameInput.UI.OpenCraft.performed -= ToggleOpen;
      _creatureFabric.OnPlayerCreated -= Init;
    }
  }
}