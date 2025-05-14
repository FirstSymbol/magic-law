using System;
using ProjectContent.Code.Csharps;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class DragItems : MonoBehaviour
  {
    [NonSerialized]
    public Slot DraggingSlot;
    [NonSerialized]
    public Inventory DraggingInventory;
  }
}