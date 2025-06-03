using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.Creatures
{
  /// <summary>
  /// Интерфейс получения урона
  /// </summary>
  public interface IDamageableEntity
  {
    public void TakeDamage(float damage, GameObject sender = null);
    
  }
}