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
    public InventoryViewLinker InventoryViewLinker;
    private StorageWindow _storageWindow;
    private UIController _uiController;

    private void Awake()
    {
    }

    private void Start()
    {
      _storageWindow = _uiController.WindowsController.GetWindow<StorageWindow>();
      InventoryViewLinker.InventoryView = _storageWindow.InventoryView;
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
        InventoryViewLinker.Unlink();
      else
        InventoryViewLinker.Link();
      _storageWindow.Toggle();
      InteractorObject = sender;
      IsInteracting = true;
    }

    [Inject]
    private void Inject(UIController uiController)
    {
      _uiController = uiController;
    }
  }
}