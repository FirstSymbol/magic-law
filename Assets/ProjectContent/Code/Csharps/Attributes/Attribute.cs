using System;
using System.Collections.Generic;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.ToolsAndExtentionsScripts.DictionarySerializer;
using ProjectContent.Code.ToolsAndExtentionsScripts.TypeSerializer;
using TriInspector;

namespace ProjectContent.Code.Csharps.Attributes
{
  [DeclareHorizontalGroup("HorizontalGroup")]
  [Serializable]
  public abstract class Attribute
  {
    public float Value;
    public List<AttributeActiveType> ActiveTypes;
    
    [ReadOnly] abstract public SerializableDictionary<TypeReference<StatBase>, float> InteractionTypes { get; protected set; }
  }
}