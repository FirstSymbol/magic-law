using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder
{
  [RequireComponent(typeof(Inventory))]
  public class Chest : Entity, IInteractableEntity
  {
    public GameObject InteractorObject { get; set; } = null;
    public bool IsInteracting { get; set; }
    public Inventory Inventory;
    public InventoryViewLinker InventoryViewLinker;
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
      InventoryViewLinker.inventoryView = _storageWindow.inventoryView;
    }
  
    public void Interact(GameObject sender)
    {
      Debug.Log("Interacting with Chest");
      // Открытие UI
      if (_storageWindow.IsOpened)
      {
        InventoryViewLinker.Unlink();
      }
      else
      {
        InventoryViewLinker.Link();
      }
      _storageWindow.Toggle();
      InteractorObject = sender;
      IsInteracting = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("InteractionTrigger") == false) return;
      if (other.transform.parent.gameObject == InteractorObject)
      {
        _uiController.WindowsController.CloseWindow(_storageWindow);
        InteractorObject = null;
        IsInteracting = false;
      }
    }
  }
}