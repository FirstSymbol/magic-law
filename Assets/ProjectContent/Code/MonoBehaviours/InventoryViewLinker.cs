using System;
using ProjectContent.Code.MonoBehaviours.UI;
using TriInspector;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  public class InventoryViewLinker : MonoBehaviour
  {
    
    public InventoryView InventoryView;
    private UIController _uiController;

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }
    private void Start()
    {
      InventoryView = _uiController.WindowsController.GetWindow<InventoryWindow>().InventoryView;
    }

    [Button("Link Inventory View")]
    public void Link(Inventory inventory)
    {
      InventoryView.Connect(inventory);
    }

    public void Unlink()
    {
      InventoryView.Disconnect();
    }
  }
}