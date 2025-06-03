using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Stats;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  /// <summary>
  ///   Отвечающий за все возможны характеристики класс.
  ///   Serializable.
  /// </summary>
  [Serializable]
  public class CreatureStats
  {
    public Health Health;
    public Mana Mana;
    public Stamina Stamina;
    public Speed Speed;
    public Armor Armor;
    public Dictionary<Type, StatBase> stats = new();

    public void Initialize()
    {
      stats.Add(typeof(Health), Health);
      stats.Add(typeof(Mana), Mana);
      stats.Add(typeof(Stamina), Stamina);
      stats.Add(typeof(Speed), Speed);
      stats.Add(typeof(Armor), Armor);
    }
  }
}