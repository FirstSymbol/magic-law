using ProjectContent.Code.Csharps;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Code.ScriptableObjects.Base;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
  public class ItemObject : UnityEngine.MonoBehaviour, IInteractableEntity
  {
    public ItemConfig itemConfig;
    public int Count;
    
    
    private void Awake()
    {
      GetComponent<SpriteRenderer>().sprite = itemConfig.Sprite;
      GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void Interact(GameObject sender)
    {
      
#if DEBUG
      Debug.Log(name + " interacted with " + sender);
#endif
      
      if (Count < 1)
      {
        
#if DEBUG
        Debug.LogWarning("Count is less than 1 because item has been destroyed");
#endif
        
        Destroy(gameObject);
      }
      else if (sender.TryGetComponent(out Inventory inventory))
      {
        inventory.AddItem(itemConfig, Count);
        Destroy(gameObject);
      }
      
    }
  }
}