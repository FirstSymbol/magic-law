using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  public class LookDirection : MonoBehaviour
  {
    public float angle = 0f;
    public Vector2 direction;
    private void Update()
    {
      var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
      
      float rad = angle * Mathf.Deg2Rad;
      float x = Mathf.Cos(rad);
      float y = Mathf.Sin(rad);
      
      direction = new Vector2(x, y);

#if DEBUG
      Debug.DrawRay(transform.position, mousePos, Color.red);
#endif
      
      
    }
  }
}