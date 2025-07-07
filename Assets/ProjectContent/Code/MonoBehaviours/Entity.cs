using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Базовый класс сущности
  /// </summary>
  [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
  public abstract class Entity : MonoBehaviour
  {
  }
}