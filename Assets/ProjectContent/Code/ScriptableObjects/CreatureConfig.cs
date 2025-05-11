using ProjectContent.Code.Csharps.Item.Usages;
using UnityEngine;

namespace ProjectContent.Code.ScriptableObjects.Base
{
  [CreateAssetMenu(fileName = "EntityConfig Default", menuName = "Configs/EntityConfig", order = 0)]
  public class CreatureConfig : ScriptableObject
  {
    public CreatureStats creatureStats;

  }
}