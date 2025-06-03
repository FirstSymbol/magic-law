using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Game_Assets.Creatures.Player.Scripts
{
  public class Player : Creature, ICanInteract, IKeepItems
  {
    [field: SerializeField] public CraftStation PlayerCraftingStation { get; private set; }

    [field: SerializeField] public Inventory FastInventory { get; private set; }
    [field: SerializeField] public Inventory MainInventory { get; private set; }

    [field: SerializeField] public InventoryViewLinker InventoryFastViewLinker { get; private set; }
    [field: SerializeField] public InventoryViewLinker InventoryMainViewLinker { get; private set; }
    private CraftWindow _craftWindow;
    private GameInput _gameInput;
    private UIController _uiController;

    private void Start()
    {
      Init();
    }

    private void OnDestroy()
    {
      _gameInput.Player.LBM.started -= UseItem;
      _gameInput.Player.RBM.started -= AlternateUseItem;
      _gameInput.UI.OpenCraft.performed -= OpenCraft;
    }

    [field: SerializeField] public Interaction Interaction { get; private set; }

    public void Interact(GameObject target = null)
    {
      Debug.Log("Player interact with " + Interaction.GetCurrentInteraction().gameObject.name);
    }

    [field: SerializeField] public SlotSelector SlotSelector { get; private set; }

    [field: SerializeField] public EquipItem EquipItem { get; private set; }

    [Inject]
    private void Inject(GameInput gameInput, UIController uiController)
    {
      _gameInput = gameInput;
      _uiController = uiController;
    }

    private void OpenCraft(InputAction.CallbackContext obj)
    {
      if (PlayerCraftingStation)
      {
      }
    }

    private void Init()
    {
      _gameInput.Player.LBM.started += UseItem;
      _gameInput.Player.RBM.started += AlternateUseItem;
      _gameInput.UI.OpenCraft.performed += OpenCraft;

      _uiController.WindowsController.GetWindow<FastPanelWindow>().InventoryView.Connect(FastInventory);
      _uiController.WindowsController.GetWindow<InventoryWindow>().InventoryView.Connect(MainInventory);
    }


    private void UseItem(InputAction.CallbackContext obj)
    {
      var t = SlotSelector.GetSelectedSlot();
      if (t != null && t.SlotData.Item != null) UseItemActions(t.SlotData.Item.usageActions);
    }

    private void AlternateUseItem(InputAction.CallbackContext obj)
    {
      var t = SlotSelector.GetSelectedSlot();
      if (t != null && t.SlotData.Item != null)
        UseItemActions(t.SlotData.Item.alternateUsageActions);
    }

    private void UseItemActions(List<UsageAction> usageActions)
    {
      foreach (var action in usageActions)
        action.Execute(gameObject);

      if (usageActions.Count > 0)
        CheckItemType();
    }

    private void CheckItemType()
    {
      var usageSlot = SlotSelector.GetSelectedSlot();
      if (usageSlot.SlotData.Item != null)
        switch (usageSlot.SlotData.Item.ItemType)
        {
          case EItemType.Consumable:
            usageSlot.SlotData.SubValue(1);
            break;
        }
    }
  }
}