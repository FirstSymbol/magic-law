using ProjectContent.Code.Csharps;
using ProjectContent.Code.PrototypingFolder;
using ProjectContent.Code.ScriptableObjects;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
  public class ItemObject : UnityEngine.MonoBehaviour, IInteractableEntity
  {
    public ItemConfig itemConfig;
    public int Count;
    public bool IsInteracting { get; set; }
    public GameObject InteractorObject { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    
    private void Awake()
    {
      SpriteRenderer = GetComponent<SpriteRenderer>();
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Initialize(ItemConfig itemConfig, int count)
    {
      this.itemConfig = itemConfig;
      SpriteRenderer.sprite = itemConfig.Sprite;
      Count = count;
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