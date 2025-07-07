using ProjectContent.Code.Csharps;
using ProjectContent.Code.ScriptableObjects;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class DefaultSlotView : SlotView
  {
    public void Connect(int index)
    {
      Index = index;
    }

    public new void UpdateSlot(ItemConfig item)
    {
      if (item == null)
      {
        Hide();
      }
      else
      {
        if (_isHide)
          Show();
        ItemImage.sprite = item.Icon;
        CountText.text = "" + item.CraftCount;
      }
    }

    public new void UpdateSlot(CraftElement craftElement)
    {
      if (craftElement.Item == null)
      {
        Hide();
      }
      else
      {
        if (_isHide)
          Show();
        ItemImage.sprite = craftElement.Item.Icon;
        CountText.text = "" + craftElement.Amount;
      }
    }
  }
}