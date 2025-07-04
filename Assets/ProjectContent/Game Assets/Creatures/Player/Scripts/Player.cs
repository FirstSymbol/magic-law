﻿using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Code.MonoBehaviours.UI;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Code.ScriptableObjects;
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
    private GameInput _gameInput;

    [field: SerializeField] public Interaction Interaction { get; private set; }

    [field: SerializeField] public SlotSelector SlotSelector { get; private set; }

    [field: SerializeField] public EquipItem EquipItem { get; private set; }
    
    [field: SerializeField] public InventoryViewLinker InventoryFastViewLinker { get; private set; }
    [field: SerializeField] public InventoryViewLinker InventoryFastMainLinker { get; private set; }
    private CraftWindow _craftWindow;
    private UIController _uiController;

    [Inject]
    private void Inject(GameInput gameInput, UIController uiController)
    {
      _gameInput = gameInput;
      _uiController = uiController;
    }

    public void Interact(GameObject target = null)
    {
      Debug.Log("Player interact with " + Interaction.GetCurrentInteraction().gameObject.name);
    }

    private void OnEnable()
    {
      _gameInput.Player.LBM.started += UseItem;
      _gameInput.Player.RBM.started += AlternateUseItem;
      _gameInput.UI.OpenCraft.performed += OpenCraft;
    }

    private void OpenCraft(InputAction.CallbackContext obj)
    {
      if (PlayerCraftingStation)
      {
        
      }
    }

    private void Start()
    {
      InventoryFastViewLinker.Link();
      InventoryFastMainLinker.Link();
    }

    private void OnDisable()
    {
      _gameInput.Player.LBM.started -= UseItem;
      _gameInput.Player.RBM.started -= AlternateUseItem;
      InventoryFastViewLinker.Unlink();
      InventoryFastMainLinker.Unlink();
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