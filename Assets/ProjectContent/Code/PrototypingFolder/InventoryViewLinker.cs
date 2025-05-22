using ProjectContent.Code.Csharps;
using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.MonoBehaviours.UI;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder
{
  public class InventoryViewLinker : MonoBehaviour
  {
    public InventoryView inventoryView;
    public Inventory inventory;

    [Button("Link Inventory View")]
    public void Link()
    {
      inventoryView.Connect(inventory);
    }

    public void Unlink()
    {
      inventoryView.Disconnect();
    }
  }
}