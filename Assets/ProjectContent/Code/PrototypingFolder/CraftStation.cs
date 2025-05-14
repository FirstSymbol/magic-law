using System;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.MonoBehaviours.UI;
using ProjectContent.Code.ScriptableObjects;
using ProjectContent.Game_Assets.Creatures.Player.Scripts;
using TriInspector;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.PrototypingFolder
{
  public class CraftStation : Entity, IInteractableEntity
  {
    public bool IsInteracting { get; set; }
    public GameObject InteractorObject { get; set; } = null;
    public CraftBundle[] CraftBundles =  new CraftBundle[1];
    private WindowsController _windowsController;
    private CraftWindow _craftWindow;
    public Inventory _interactorInventory;
    
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
    public void Craft(int craftBundleIndex,int index)
    {
      int amount = CraftBundles[craftBundleIndex].Items[index].CraftCount;
      bool check = true;
      
      if (_interactorInventory == null) return;
      
      foreach (CraftElement craftElement in CraftBundles[craftBundleIndex].Items[index].craftCoats)
        if (_interactorInventory.GetItemCount(craftElement.Item) < craftElement.Amount * amount) 
          check = false;
      
      if (!check) return;
      
      foreach (CraftElement craftElement in CraftBundles[craftBundleIndex].Items[index].craftCoats) 
        _interactorInventory.RemoveItem(craftElement.Item, craftElement.Amount);
      
      _interactorInventory.AddItem(CraftBundles[craftBundleIndex].Items[index], amount);
      
      Debug.Log($"Craft: {CraftBundles[craftBundleIndex].Items[index].name}");
    }
    
    public void Interact(GameObject sender)
    {
      Debug.Log("Interacting with CraftStation");
      if (sender.TryGetComponent(out Player player))
      {
        _interactorInventory = player.MainInventory;
      }
      _windowsController.OpenWindow<CraftWindow>();
      _craftWindow.View.Connect(this);
      InteractorObject = sender;
      IsInteracting = true;
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
  }
}