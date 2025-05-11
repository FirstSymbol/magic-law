using System;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.Csharps.Attributes;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using static ProjectContent.Code.Csharps.Enums.ConstValues.Animation;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
  [RequireComponent(typeof(Animator))]
  public class EquipItem : MonoBehaviour
  {
    public LookDirection lookDirection;
    public SpriteRenderer spriteRenderer;
    public float offset = 0.35f;
    public Animator animator;
    public SlotSelector slotSelector;
    public Creature creature;
    public BoxCollider2D boxCollider2D;
    [NonSerialized] public Slot EquippedItem;
    [NonSerialized] public SlotData LastEquippedItemData;
    public Action<Slot> OnItemAlternativeUsed;

    public Action<Slot> OnItemEquipped;
    public Action<Slot> OnItemMainUsed;
    public Action<Slot> OnItemUnequipped;

    private void Start()
    {
      UpdateItemVisual();
      EquipmentItem(slotSelector.GetSelectedSlot());
    }

    private void Update()
    {
      UpdatePosition();
      UpdateMirror();
    }

    private void OnEnable()
    {
      slotSelector.SlotSwitched += ItemSwitched;
      slotSelector.Inventory.OnSlotSetted += OnSlotSetted;
      slotSelector.Inventory.OnSlotFill += OnSlotFill;
      slotSelector.Inventory.OnSlotUpdated += UpdateItemVisual;
    }

    private void OnDisable()
    {
      slotSelector.SlotSwitched -= ItemSwitched;
      slotSelector.Inventory.OnSlotSetted -= OnSlotSetted;
      slotSelector.Inventory.OnSlotUpdated -= UpdateItemVisual;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      var t = slotSelector.GetSelectedSlot();
      if (t != null && t.SlotData.Item != null)
        if (t.SlotData.Item.ItemType == EItemType.Weapon)
          if (other.TryGetComponent(out IDamageableEntity damageableCreature))
            damageableCreature.TakeDamage(t.SlotData.Item.damage, creature.gameObject);
    }

    private void OnSlotSetted(int index)
    {
      if (index == slotSelector.SelectedSlotIndex)
      {
        EquipmentAndUnequip(EquippedItem);
      }//
    }
    private void OnSlotFill(int index)
    {
      if (index == slotSelector.SelectedSlotIndex)
      {
        EquipmentItem(EquippedItem);
      }//
    }
    private void ItemSwitched(Slot slot)
    {
      EquipmentAndUnequip(slot);
    }
    
    private void EquipmentSlotSet(Slot B)
    {
      EquipmentAndUnequip(B);
    }

    private void EquipmentAndUnequip(Slot slot)
    {
      if (LastEquippedItemData != null)
        UnequipmentItem();
      
      EquipmentItem(slot);
    }
    
    private void EquipmentItem(Slot slot)
    {
      EquippedItem = slot;
      LastEquippedItemData = slot.SlotData;
      
      // Логика с атрибутами
      if (EquippedItem.SlotData.Item != null)
        foreach (var attribute in EquippedItem.SlotData.Item.attributes)
          if (attribute.ActiveTypes.Contains(AttributeActiveType.WhenInHand))
            foreach (var stat in attribute.InteractionTypes.Keys)
              if (creature.creatureStats.stats.ContainsKey(stat.Type))
                creature.creatureStats.stats[stat.Type].AddValue(attribute.Value * attribute.InteractionTypes[stat]);

      UpdateItemVisual();
    }

    private void UnequipmentItem(int index = 0)
    {
      UnequipmentItemBySlot(LastEquippedItemData);
    }

    private void UnequipmentItemBySlot(SlotData slotData)
    {
      // Логика с атрибутами
      if (slotData.Item != null)
        foreach (var attribute in slotData.Item.attributes)
          if (attribute.ActiveTypes.Contains(AttributeActiveType.WhenInHand))
            foreach (var stat in attribute.InteractionTypes.Keys)
              if (creature.creatureStats.stats.ContainsKey(stat.Type))
                creature.creatureStats.stats[stat.Type].SubstractValue(attribute.Value * attribute.InteractionTypes[stat]);
      LastEquippedItemData = null;
      EquippedItem = null;
      UpdateItemVisual();
    }

    private void UpdateItemVisual(int index = 0)
    {
      if (EquippedItem == null || EquippedItem.SlotData.Item == null)
        ClearSpriteAndAnimation();
      else
        ApplySpriteAndAnimation();
    }

    public void MainUseItem()
    {
      if (animator.runtimeAnimatorController != null)
        animator.SetBool(IsUse, true);
      OnItemMainUsed?.Invoke(EquippedItem);
    }

    public void AlternateUseItem()
    {
      if (animator.runtimeAnimatorController != null)
        animator.SetBool(IsAlternativeUse, true);
      OnItemAlternativeUsed?.Invoke(EquippedItem);
    }

    private void ClearSpriteAndAnimation()
    {
      animator.runtimeAnimatorController = null;
      spriteRenderer.sprite = null;
    }

    private void ApplySpriteAndAnimation()
    {
      animator.runtimeAnimatorController = EquippedItem.SlotData.Item.AnimatorController;
      spriteRenderer.sprite = EquippedItem.SlotData.Item.Sprite;
    }

    private void UpdateMirror()
    {
      if (lookDirection.direction.x < 0)
        transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
      else if (lookDirection.direction.x > 0)
        transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
    }

    private void UpdatePosition()
    {
      transform.localEulerAngles = Vector3.forward * lookDirection.angle;
      transform.localPosition = new Vector3(lookDirection.direction.x * offset, lookDirection.direction.y * offset,
        transform.localPosition.z);
    }
  }
}