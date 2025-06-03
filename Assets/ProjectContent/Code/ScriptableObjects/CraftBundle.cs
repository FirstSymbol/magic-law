using System.Collections.Generic;
using UnityEngine;

namespace ProjectContent.Code.ScriptableObjects
{
  [CreateAssetMenu(fileName = "CraftBundle Default", menuName = "Configs/CraftBundle", order = 0)]
  public class CraftBundle : ScriptableObject
  {
    public List<ItemConfig> Items = new();
  }
}