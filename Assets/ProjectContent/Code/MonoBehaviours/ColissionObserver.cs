using System;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(Collider2D))]
  public class ColissionObserver : UnityEngine.MonoBehaviour
  {
    public Action<GameObject> OnColliderEnter;
    public Action<GameObject> OnColliderExit;
    public Action<GameObject> OnTriggerExit;
    public Action<GameObject> OnTriggerEnter;

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