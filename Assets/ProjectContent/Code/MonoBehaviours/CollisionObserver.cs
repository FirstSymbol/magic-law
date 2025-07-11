﻿using System;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Скрипт обработки соприкосновений
  /// </summary>
  [RequireComponent(typeof(Collider2D))]
  public class CollisionObserver : MonoBehaviour
  {
    public Action<GameObject> OnColliderEnter;
    public Action<GameObject> OnColliderExit;
    public Action<GameObject> OnTriggerEnter;
    public Action<GameObject> OnTriggerExit;

    private void OnCollisionEnter2D(Collision2D other)
    {
      OnColliderEnter?.Invoke(other.gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
      OnColliderExit?.Invoke(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      OnTriggerEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      OnTriggerExit?.Invoke(other.gameObject);
    }
  }
}