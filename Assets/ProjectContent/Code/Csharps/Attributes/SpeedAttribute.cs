using System;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.ToolsAndExtentionsScripts.DictionarySerializer;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Attributes
{
  [Serializable]
  public class SpeedAttribute : Attribute
  {
    [field: SerializeField]
    [field: ReadOnly]
    public override SerializableDictionary<TypeReference<StatBase>, float> InteractionTypes { get; protected set; } =
      new()
      {
        { new TypeReference<StatBase>(typeof(Speed)), 1f }
      };
  }
}