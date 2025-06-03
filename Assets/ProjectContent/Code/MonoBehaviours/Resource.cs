using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Скрипт генератора ресурсов
  /// </summary>
  public class Resource : MonoBehaviour, IDamageableEntity
  {
    public SlotData ResourceData;
    public int ExtractionCount = 1;

    public void TakeDamage(float damage, GameObject sender = null)
    {
      if (sender != null)
        if (sender.TryGetComponent(out Inventory inventory))
          inventory.AddItem(ResourceData.Item, ExtractionCount);

      ResourceData.Count -= ExtractionCount;

      if (ResourceData.Count <= 0) Destroy(gameObject);
    }
  }
}