using ProjectContent.Code.Csharps;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class CraftWindow : UIWindow
  {
    public CraftView View;
    private CreatureFabric _creatureFabric;
    private GameInput _gameInput;
    private Player _player;

    private void OnDestroy()
    {
      _gameInput.UI.OpenCraft.performed -= ToggleOpen;
      _creatureFabric.OnPlayerCreated -= OnPlayerCreated;
    }

    [Inject]
    private void Inject(GameInput gameInput, CreatureFabric creatureFabric)
    {
      _creatureFabric = creatureFabric;
      _gameInput = gameInput;
    }

    public override void Initialize()
    {
      if (_creatureFabric.Player != null)
        OnPlayerCreated();
      _creatureFabric.OnPlayerCreated += OnPlayerCreated;

      _gameInput.UI.OpenCraft.performed += ToggleOpen;
      gameObject.SetActive(false);
    }

    private void OnPlayerCreated()
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
  }
}