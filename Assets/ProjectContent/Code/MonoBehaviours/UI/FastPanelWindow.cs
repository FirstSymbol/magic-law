namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class FastPanelWindow : UIWindow
  {
    public InventoryView InventoryView;

    public override void Initialize()
    {
      gameObject.SetActive(true);
    }

    public override void Toggle()
    {
    }
  }
}