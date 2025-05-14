namespace ProjectContent.Code.MonoBehaviours.UI
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
      if (IsOpened)
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