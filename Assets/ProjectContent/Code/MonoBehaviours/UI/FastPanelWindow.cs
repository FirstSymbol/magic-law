using ProjectContent.Code.MonoBehaviours.UI;

namespace ProjectContent.Code.PrototypingFolder.UI
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