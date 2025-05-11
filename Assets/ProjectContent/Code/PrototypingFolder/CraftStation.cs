using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.PrototypingFolder.Good;
using ProjectContent.Code.PrototypingFolder.UI;
using ProjectContent.Code.ScriptableObjects;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using TriInspector;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder
{
  public class CraftStation : Entity, IInteractableEntity
  {
    public CraftBundle CraftBundle;
    private WindowsController _windowsController;
    private CraftWindow _craftWindow;
    private Inventory _interactorInventory;
    
    [Inject]
    private void Inject(UIController uiController)
    {
      _windowsController = uiController.WindowsController;
    }

    private void Start()
    {
      _craftWindow = _windowsController.GetWindow<CraftWindow>();
    }

    [Button("Craft Debug")]
    public void Craft(int index)
    {
      int amount = CraftBundle.Items[index].CraftCount;
      bool check = true;
      
      if (_interactorInventory == null) return;
      
      foreach (CraftElement craftElement in CraftBundle.Items[index].craftCoats)
        if (_interactorInventory.GetItemCount(craftElement.Item) < craftElement.Amount * amount) 
          check = false;
      
      if (!check) return;
      
      _interactorInventory.AddItem(CraftBundle.Items[index], amount);
      Debug.Log($"Craft: {CraftBundle.Items[index].name}");
    }
    
    public void Interact(GameObject sender)
    {
      Debug.Log("Interacting with CraftStation");
      if (sender.TryGetComponent(out Player player))
      {
        _interactorInventory = player.MainInventory;
      }
      _windowsController.OpenWindow<CraftWindow>();
      _craftWindow.view.Connect(this);
    }
  }
}