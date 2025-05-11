using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectContent.Code.PrototypingFolder
{
  public class Resource : MonoBehaviour, IDamageableEntity
  {
    [FormerlySerializedAs("ResourceSlot")] public SlotData ResourceData;
    public int ExtractionCount = 1;
    
    
    
    public void TakeDamage(float damage, GameObject sender = null)
    {
      Debug.Log(gameObject.name + " taking damage");
      
      
      
      if (sender != null)
        if (sender.TryGetComponent(out Inventory inventory))
          inventory.AddItem(ResourceData.Item, ExtractionCount);
      
      ResourceData.Count -= ExtractionCount;
      
      if (ResourceData.Count <= 0) Destroy(gameObject);
      
    }

    
  }
}