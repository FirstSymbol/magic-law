using ProjectContent.Code.Csharps;
using ProjectContent.Code.ScriptableObjects;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Компонент предмета в мире
  /// </summary>
  [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
  public class ItemObject : MonoBehaviour, IInteractableEntity
  {
    public ItemConfig ItemConfig;
    public int Count;
    public SpriteRenderer SpriteRenderer { get; set; }


    private void Awake()
    {
      SpriteRenderer = GetComponent<SpriteRenderer>();
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public bool IsInteracting { get; set; }
    public GameObject InteractorObject { get; set; }

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
        inventory.AddItem(ItemConfig, Count);
        Destroy(gameObject);
      }
    }

    public void Initialize(ItemConfig itemConfig, int count)
    {
      ItemConfig = itemConfig;
      SpriteRenderer.sprite = itemConfig.Sprite;
      Count = count;
    }
  }
}