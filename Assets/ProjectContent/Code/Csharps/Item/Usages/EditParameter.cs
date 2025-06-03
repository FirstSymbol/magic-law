using System;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Item.Usages
{
  /// <summary>
  ///   Действие использования отвечающее за изменение характеристики/параметра на определенную единицу.
  /// </summary>
  [Serializable]
  public class EditParameter : UsageAction
  {
    public TypeReference<StatBase> payloadType;
    public float changeValue;

    public override void Execute(GameObject sender)
    {
      if (sender.TryGetComponent(out Creature creature))
      {
        var stat = creature.CreatureStats.stats[payloadType.Type];
        stat.AddValue(changeValue);

#if DEBUG
        Debug.Log(payloadType.Type + " - " + stat.Value);
#endif
      }
    }
  }
}