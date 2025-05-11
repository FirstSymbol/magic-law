using System;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.PrototypingFolder.Good;
using ProjectContent.Code.PrototypingFolder.UI;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder
{
  [RequireComponent(typeof(Inventory))]
  public class Chest : Entity, IInteractableEntity
  {
    public Inventory inventory;
    public InventoryViewLinker inventoryViewLinker;
    private UIController _uiController;
    private StorageWindow _storageWindow;

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }

    private void Awake()
    {
      
      
    }

    private void Start()
    {
      _storageWindow = _uiController.WindowsController.GetWindow<StorageWindow>();
      inventoryViewLinker.inventoryView = _storageWindow.inventoryView;
    }
  
    public void Interact(GameObject sender)
    {
      Debug.Log("Interacting with Chest");
      // Открытие UI
      
      
      if (_storageWindow.IsOpened)
      {
        inventoryViewLinker.Unlink();
      }
      else
      {
        inventoryViewLinker.Link();
      }
      _storageWindow.Toggle();
      
      
    }
  }
}