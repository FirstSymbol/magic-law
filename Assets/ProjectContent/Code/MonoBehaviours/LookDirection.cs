using System;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  public class LookDirection : MonoBehaviour
  {
    public float Angle = 0f;
    public Vector2 Direction;
    public Camera Camera;

    private void Start()
    {
      if (Camera == null)
      {
        Camera = Camera.main;
#if DEBUG
        Debug.LogWarning("LookDirection: Камер не найдена, по этому она была установлена в главную камеру по умолчанию!");
#endif
      }
    }

    private void Update()
    {
      var mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
      Angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
      
      float rad = Angle * Mathf.Deg2Rad;
      float x = Mathf.Cos(rad);
      float y = Mathf.Sin(rad);
      
      Direction = new Vector2(x, y);

#if DEBUG
      Debug.DrawRay(transform.position, mousePos, Color.red);
#endif
      
      
    }
  }
}