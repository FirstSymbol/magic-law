using ProjectContent.Code.MonoBehaviours.UI;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours
{
  public class InventoryViewLinker : MonoBehaviour
  {
    public InventoryView InventoryView;
    public Inventory Inventory;

    [Button("Link Inventory View")]
    public void Link()
    {
      InventoryView.Connect(Inventory);
    }

    public void Unlink()
    {
      InventoryView.Disconnect();
    }
  }
}