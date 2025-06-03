using System;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.Csharps.Attributes;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using UnityEngine.Serialization;
using static ProjectContent.Code.Csharps.Enums.ConstValues.Animation;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  /// Класс управления взятым предметом. Отвечает за его отображение, анимации и прочее.
  /// </summary>
  [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
  [RequireComponent(typeof(Animator))]
  public class EquipItem : MonoBehaviour
  {
    public LookDirection LookDirection;
    public SpriteRenderer SpriteRenderer;
    public float Offset = 0.35f;
    public Animator Animator;
    public SlotSelector SlotSelector;
    public Creature Creature;
    public BoxCollider2D BoxCollider2D;

    public Action<Slot> OnItemAlternativeUsed;
    public Action<Slot> OnItemEquipped;
    public Action<Slot> OnItemMainUsed;
    public Action<Slot> OnItemUnequipped;

    [NonSerialized] private Slot _equippedItem;
    [NonSerialized] private SlotData _lastEquippedItemData;
    
    private void Start()
    {
      UpdateItemVisual();
      EquipmentItem(SlotSelector.GetSelectedSlot());
    }

    private void Update()
    {
      UpdatePosition();
      UpdateMirror();
    }

    private void OnEnable()
    {
      SlotSelector.SlotSwitched += ItemSwitched;
      SlotSelector.Inventory.OnSlotSetted += OnSlotSetted;
      SlotSelector.Inventory.OnSlotFill += OnSlotFill;
      SlotSelector.Inventory.OnSlotUpdated += UpdateItemVisual;
    }

    private void OnDisable()
    {
      SlotSelector.SlotSwitched -= ItemSwitched;
      SlotSelector.Inventory.OnSlotSetted -= OnSlotSetted;
      SlotSelector.Inventory.OnSlotUpdated -= UpdateItemVisual;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      var t = SlotSelector.GetSelectedSlot();
      if (t != null && t.SlotData.Item != null)
        if (t.SlotData.Item.ItemType == EItemType.Weapon)
          if (other.TryGetComponent(out IDamageableEntity damageableCreature))
            damageableCreature.TakeDamage(t.SlotData.Item.damage, Creature.gameObject);
    }

    private void OnSlotSetted(int index)
    {
      if (index == SlotSelector.SelectedSlotIndex)
      {
        EquipmentAndUnequip(_equippedItem);
      }//
    }
    private void OnSlotFill(int index)
    {
      if (index == SlotSelector.SelectedSlotIndex)
      {
        EquipmentItem(_equippedItem);
      }
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
      if (_lastEquippedItemData != null)
        UnequipmentItem();
      
      EquipmentItem(slot);
    }
    
    private void EquipmentItem(Slot slot)
    {
      _equippedItem = slot;
      _lastEquippedItemData = slot.SlotData;
      
      // Логика с атрибутами
      if (_equippedItem.SlotData.Item != null)
        foreach (var attribute in _equippedItem.SlotData.Item.attributes)
          if (attribute.ActiveTypes.Contains(AttributeActiveType.WhenInHand))
            foreach (var stat in attribute.InteractionTypes.Keys)
              if (Creature.CreatureStats.stats.ContainsKey(stat.Type))
                Creature.CreatureStats.stats[stat.Type].AddValue(attribute.Value * attribute.InteractionTypes[stat]);

      UpdateItemVisual();
    }

    private void UnequipmentItem(int index = 0)
    {
      UnequipmentItemBySlot(_lastEquippedItemData);
    }

    private void UnequipmentItemBySlot(SlotData slotData)
    {
      // Логика с атрибутами
      if (slotData.Item != null)
        foreach (var attribute in slotData.Item.attributes)
          if (attribute.ActiveTypes.Contains(AttributeActiveType.WhenInHand))
            foreach (var stat in attribute.InteractionTypes.Keys)
              if (Creature.CreatureStats.stats.ContainsKey(stat.Type))
                Creature.CreatureStats.stats[stat.Type].SubstractValue(attribute.Value * attribute.InteractionTypes[stat]);
      _lastEquippedItemData = null;
      _equippedItem = null;
      UpdateItemVisual();
    }

    private void UpdateItemVisual(int index = 0)
    {
      if (_equippedItem == null || _equippedItem.SlotData.Item == null)
        ClearSpriteAndAnimation();
      else
        ApplySpriteAndAnimation();
    }

    // ПКМ
    public void MainUseItem()
    {
      if (Animator.runtimeAnimatorController != null)
        Animator.SetBool(IsUse, true);
      OnItemMainUsed?.Invoke(_equippedItem);
    }

    // ЛКМ
    public void AlternateUseItem()
    {
      if (Animator.runtimeAnimatorController != null)
        Animator.SetBool(IsAlternativeUse, true);
      OnItemAlternativeUsed?.Invoke(_equippedItem);
    }

    // Очистка анимаций
    private void ClearSpriteAndAnimation()
    {
      Animator.runtimeAnimatorController = null;
      SpriteRenderer.sprite = null;
    }

    // Установка анимаций
    private void ApplySpriteAndAnimation()
    {
      Animator.runtimeAnimatorController = _equippedItem.SlotData.Item.AnimatorController;
      SpriteRenderer.sprite = _equippedItem.SlotData.Item.Sprite;
    }
    
    // Отражение предмета при поворотах
    private void UpdateMirror()
    {
      if (LookDirection.Direction.x < 0)
        transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
      else if (LookDirection.Direction.x > 0)
        transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
    }
    
    // Обновление позиции предмета
    private void UpdatePosition()
    {
      transform.localEulerAngles = Vector3.forward * LookDirection.Angle;
      transform.localPosition = new Vector3(LookDirection.Direction.x * Offset, LookDirection.Direction.y * Offset,
        transform.localPosition.z);
    }
  }
}