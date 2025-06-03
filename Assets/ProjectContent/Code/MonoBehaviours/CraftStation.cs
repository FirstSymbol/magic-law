using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.UI;
using ProjectContent.Code.ScriptableObjects;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using TriInspector;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  public class CraftStation : Entity, IInteractableEntity
  {
    public CraftBundle[] CraftBundles = new CraftBundle[1];
    public Inventory _interactorInventory;
    private CraftWindow _craftWindow;
    private WindowsController _windowsController;

    private void Start()
    {
      _craftWindow = _windowsController.GetWindow<CraftWindow>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("InteractionTrigger") == false) return;
      if (other.transform.parent.gameObject == InteractorObject)
      {
        _windowsController.CloseWindow(_craftWindow);
        InteractorObject = null;
        IsInteracting = false;
      }
    }

    public bool IsInteracting { get; set; }
    public GameObject InteractorObject { get; set; }

    public void Interact(GameObject sender)
    {
      Debug.Log("Interacting with CraftStation");
      if (sender.TryGetComponent(out Player player)) _interactorInventory = player.FastInventory;
      _windowsController.OpenWindow<CraftWindow>();
      _craftWindow.View.Connect(this);
      InteractorObject = sender;
      IsInteracting = true;
    }

    [Inject]
    private void Inject(UIController uiController)
    {
      _windowsController = uiController.WindowsController;
    }

    [Button("Craft Debug")]
    public void Craft(int craftBundleIndex, int index)
    {
      var amount = CraftBundles[craftBundleIndex].Items[index].CraftCount;
      var check = true;

      if (_interactorInventory == null) return;

      foreach (var craftElement in CraftBundles[craftBundleIndex].Items[index].craftCoats)
        if (_interactorInventory.GetItemCount(craftElement.Item) < craftElement.Amount * amount)
          check = false;

      if (!check) return;

      foreach (var craftElement in CraftBundles[craftBundleIndex].Items[index].craftCoats)
        _interactorInventory.RemoveItem(craftElement.Item, craftElement.Amount);

      _interactorInventory.AddItem(CraftBundles[craftBundleIndex].Items[index], amount);

#if DEBUG
      Debug.Log($"Craft: {CraftBundles[craftBundleIndex].Items[index].name}");
#endif
    }
  }
}