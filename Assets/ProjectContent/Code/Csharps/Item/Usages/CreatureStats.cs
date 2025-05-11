using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Stats;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  [Serializable]
  public class CreatureStats
  {
    public Dictionary<Type, StatBase> stats = new Dictionary<Type, StatBase>();

    public void Initialize()
    {
      stats.Add(typeof(Health), Health);
      stats.Add(typeof(Mana), Mana);
      stats.Add(typeof(Stamina), Stamina);
      stats.Add(typeof(Speed), Speed);
      stats.Add(typeof(Armor), Armor);
    }
    public Health Health;
    public Mana Mana;
    public Stamina Stamina;
    public Speed Speed;
    public Armor Armor;
  }
}