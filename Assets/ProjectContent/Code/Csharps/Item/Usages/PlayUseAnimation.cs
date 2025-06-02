using UnityEngine;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  /// <summary>
  /// Действие использования предмета отвечающее за проигрывание анимации использования предмета.
  /// </summary>
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