using ProjectContent.Code.Csharps.Enums;
using ProjectContent.Code.Csharps.Item.Usages;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.Creatures
{
  /// <summary>
  /// Базовый скрипт Creature. Содержит: Type и creatureStats
  /// </summary>
  [RequireComponent(typeof(CreatureAnimationController))]
  public abstract class Creature : Entity
  {
    [field: SerializeField] public ECreatureType Type { get; private set; }
    
    public CreatureStats CreatureStats = new();

    protected virtual void Awake()
    {
      CreatureStats.Initialize();
    }
  }

  public interface IDoingDamageCreature
  {
    public void Damage(IDamageableEntity target);
  }
}