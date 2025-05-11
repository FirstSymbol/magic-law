using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;

namespace ProjectContent.Game_Assets.Creatures.Dummy.Scripts
{
  public class Dummy : Creature, IDamageableEntity
  {
    public void TakeDamage(float damage, GameObject sender = null)
    {
      creatureStats.stats[typeof(Health)].SubstractValue(damage);
    }
  }
}