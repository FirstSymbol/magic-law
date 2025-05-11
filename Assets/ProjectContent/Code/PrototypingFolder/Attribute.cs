using System;
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
  [DeclareHorizontalGroup("HorizontalGroup")]
  [Serializable]
  public abstract class Attribute
  {
    public float Value;
    public List<AttributeActiveType> ActiveTypes;
    
    [ReadOnly] abstract public SerializableDictionary<TypeReference<StatBase>, float> InteractionTypes { get; protected set; }
  }
}