using System.Collections.Generic;
using ProjectContent.Code.ScriptableObjects.Base;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder
{
  [CreateAssetMenu(fileName = "CraftBundle Default", menuName = "Configs/CraftBundle", order = 0)]
  public class CraftBundle : ScriptableObject
  {
    public List<ItemConfig> Items = new List<ItemConfig>();
  }
}