using ProjectContent.Code.Csharps.Enums;
using ProjectContent.Code.Csharps.Item.Usages;
using ProjectContent.Code.PrototypingFolder;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.Creatures
{
  [RequireComponent(typeof(CreatureAnimationController))]
  public abstract class Creature : Entity
  {
    [field: SerializeField] public ECreatureType Type { get; private set; }
    
    public CreatureStats creatureStats = new();

    protected virtual void Awake()
    {
      creatureStats.Initialize();
    }
  }

  public interface IDoingDamageCreature
  {
    public void Damage(IDamageableEntity target);
  }
}