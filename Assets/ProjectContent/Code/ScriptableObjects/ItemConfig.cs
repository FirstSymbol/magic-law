using System.Collections.Generic;
using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
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
    public List<CraftElement> craftCoats = new();
    public GameObject Prefab;

    [SerializeReference] public List<UsageAction> usageActions = new();
    [SerializeReference] public List<UsageAction> alternateUsageActions = new();
    [SerializeReference] public List<Attribute> attributes = new();

    public void Drop(Vector3 position, int amount)
    {
      var item = Instantiate(Prefab, position, Quaternion.identity);
      var itemObject = item.GetComponent<ItemObject>();
      itemObject.Initialize(this, amount);
    }
  }
}