using System;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.ToolsAndExtentionsScripts.DictionarySerializer;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Attributes
{
  [Serializable]
  public class DamageAttribute : Attribute
  {
    [field: SerializeField, ReadOnly]
    public override SerializableDictionary<TypeReference<StatBase>, float> InteractionTypes { get; protected set; } =
      new();
  }
}