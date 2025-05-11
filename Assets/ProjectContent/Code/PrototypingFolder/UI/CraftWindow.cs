using System;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class CraftWindow : UIWindow
  {
    private GameInput _gameInput;
    public CraftView view;
    
    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }
    public override void Initialize()
    {
      _gameInput.UI.OpenCraft.performed += ToggleOpen;
      gameObject.SetActive(false);
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

    private void OnDestroy()
    {
      _gameInput.UI.OpenCraft.performed -= ToggleOpen;
    }
  }
}