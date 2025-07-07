using UnityEngine.Serialization;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class StorageWindow : UIWindow
  {
    [FormerlySerializedAs("inventoryView")]
    public InventoryView InventoryView;

    public override void Initialize()
    {
      gameObject.SetActive(false);
    }

    public override void Toggle()
    {
      if (IsOpened)
        _windowsController.CloseWindow(this);
      else
        _windowsController.OpenWindow<StorageWindow>();
    }
  }
}