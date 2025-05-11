using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.Creatures
{
  public interface IDamageableEntity
  {
    public void TakeDamage(float damage, GameObject sender = null);
    
  }
}