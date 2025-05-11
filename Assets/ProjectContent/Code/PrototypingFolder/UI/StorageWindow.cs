namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class StorageWindow : UIWindow
  {
    public InventoryView inventoryView;
    public override void Initialize()
    {
      gameObject.SetActive(false);
    }

    public override void Toggle()
    {
      if (isOpened)
      {
        _windowsController.CloseWindow(this);
      }
      else
      {
        _windowsController.OpenWindow<StorageWindow>();
      }
    }
  }
}