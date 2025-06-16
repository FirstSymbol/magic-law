using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(Inventory))]
  public class Chest : Entity, IInteractableEntity
  {
    public Inventory Inventory;
    private StorageWindow _storageWindow;
    private UIController _uiController;
    private InventoryViewLinker _inventoryViewLinker;

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }

    private void Start()
    {
      _inventoryViewLinker = _uiController.InventoryViewLinker;
      _storageWindow = _uiController.WindowsController.GetWindow<StorageWindow>();
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

    public GameObject InteractorObject { get; set; }

    public bool IsInteracting { get; set; }

    public void Interact(GameObject sender)
    {
      Debug.Log("Interacting with Chest");
      // Открытие UI
      if (_storageWindow.IsOpened)
        _inventoryViewLinker.Unlink();
      else
        _inventoryViewLinker.Link(Inventory);
      _storageWindow.Toggle();
      InteractorObject = sender;
      IsInteracting = true;
    }
  }
}