using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class InventoryWindow : UIWindow
  {
    public CraftView CraftView;
    
    
    private GameInput _gameInput;
    
    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }


    public override void Initialize()
    {
      _gameInput.UI.OpenInventory.performed += ToggleOpen;;
      gameObject.SetActive(false);
    }

    public override void Toggle()
    {
      if (isOpened)
      {
        _windowsController.CloseWindow(this);
      }
      else
      {
        _windowsController.OpenWindow<InventoryWindow>();
      }
    }

    private void ToggleOpen(InputAction.CallbackContext obj)
    {
      Toggle();
    }

    private void OnDestroy()
    {
      _gameInput.UI.OpenInventory.performed -= ToggleOpen;
    }
    
    
  }
}