using ProjectContent.Code.MonoBehaviours;

namespace ProjectContent.Code.Csharps
{
  /// <summary>
  /// Интерфейс использования предмета
  /// </summary>
  public interface IKeepItems
  {
    public SlotSelector SlotSelector { get; }
    public EquipItem EquipItem { get; }
  }
}