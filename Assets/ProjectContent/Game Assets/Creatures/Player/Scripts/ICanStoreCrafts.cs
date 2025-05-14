using System.Collections.Generic;
using ProjectContent.Code.ScriptableObjects;

namespace ProjectContent.Game_Assets.Creatures.Player.Scripts
{
  public interface ICanStoreCrafts
  {
    public CraftBundle CraftBundle {get;}
    public HashSet<CraftBundle> Crafts { get; }
  }
}