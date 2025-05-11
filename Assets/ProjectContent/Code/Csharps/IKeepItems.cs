using ProjectContent.Code.MonoBehaviours;
using ProjectContent.Code.PrototypingFolder;

namespace ProjectContent.Code.Csharps
{
  public interface IKeepItems
  {
    public SlotSelector SlotSelector { get; }
    public EquipItem EquipItem { get; }
  }
}