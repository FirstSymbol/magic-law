using System;
using ProjectContent.Code.Csharps;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class DragItems : MonoBehaviour
  {
    [NonSerialized] public Inventory DraggingInventory;

    [NonSerialized] public Slot DraggingSlot;
  }
}