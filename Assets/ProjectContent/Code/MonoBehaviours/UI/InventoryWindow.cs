﻿using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class InventoryWindow : UIWindow
  {
    public InventoryView InventoryView;
    private GameInput _gameInput;

    private void OnDestroy()
    {
      _gameInput.UI.OpenInventory.performed -= ToggleOpen;
    }

    [Inject]
    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }


    public override void Initialize()
    {
      _gameInput.UI.OpenInventory.performed += ToggleOpen;
      ;
      gameObject.SetActive(false);
    }

    public override void Toggle()
    {
      if (IsOpened)
        _windowsController.CloseWindow(this);
      else
        _windowsController.OpenWindow<InventoryWindow>();
    }

    private void ToggleOpen(InputAction.CallbackContext obj)
    {
      Toggle();
    }
  }
}