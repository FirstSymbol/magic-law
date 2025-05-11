using System.Collections.Generic;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.ToolsAndExtentionsScripts;
using ProjectContent.Code.ToolsAndExtentionsScripts;
using ProjectContent.Code.ToolsAndExtentionsScripts.DictionarySerializer;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder
{
  public class ArmorAttribute : Attribute
  {

    [field: SerializeField, ReadOnly]
    public override SerializableDictionary<TypeReference<StatBase>, float> InteractionTypes { get; protected set; } =
      new ()
      {
        { new TypeReference<StatBase>(typeof(Armor)), 1f }
      };
  }
}