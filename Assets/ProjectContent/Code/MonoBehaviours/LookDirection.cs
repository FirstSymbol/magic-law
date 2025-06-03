using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  public class LookDirection : MonoBehaviour
  {
    public float Angle;
    public Vector2 Direction;
    public Camera Camera;

    private void Start()
    {
      if (Camera == null)
      {
        Camera = Camera.main;
#if DEBUG
        Debug.LogWarning(
          "LookDirection: Камера не найдена, по этому она была установлена в главную камеру по умолчанию!");
#endif
      }
    }

    private void Update()
    {
      var mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
      Angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;

      var rad = Angle * Mathf.Deg2Rad;
      var x = Mathf.Cos(rad);
      var y = Mathf.Sin(rad);

      Direction = new Vector2(x, y);

#if DEBUG
      Debug.DrawRay(transform.position, mousePos, Color.red);
#endif
    }
  }
}