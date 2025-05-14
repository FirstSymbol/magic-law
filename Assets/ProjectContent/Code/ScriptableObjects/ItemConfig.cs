using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using UnityEngine;
using Attribute = ProjectContent.Code.Csharps.Attributes.Attribute;

namespace ProjectContent.Code.ScriptableObjects
{
  [CreateAssetMenu(fileName = "ItemConfig Default", menuName = "Configs/ItemConfig", order = 0)]
  public class ItemConfig : ScriptableObject
  {
    public string Name;
    public float damage = 10f;
    public int CraftCount = 1;
    public EItemType ItemType;
    public Sprite Sprite;
    public Sprite Icon;
    public int MaxCount = 32;
    public RuntimeAnimatorController AnimatorController;
    public List<CraftElement> craftCoats = new List<CraftElement>();
    
    [SerializeReference] public List<UsageAction> usageActions = new List<UsageAction>();
    [SerializeReference] public List<UsageAction> alternateUsageActions = new List<UsageAction>();
    [SerializeReference] public List<Attribute> attributes = new List<Attribute>();
    
  }

  // Usage target & usage self
  // 
}