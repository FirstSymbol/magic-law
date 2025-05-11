using UnityEngine;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  public class PlayUseAnimation : UsageAction
  {
    public override void Execute(GameObject target)
    {
      if (target.TryGetComponent(out IKeepItems keepItems))
      {
        keepItems.EquipItem.MainUseItem();
      }
    }
  }
}