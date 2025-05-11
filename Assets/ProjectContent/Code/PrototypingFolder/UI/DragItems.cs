using System;
using ProjectContent.Code.Csharps;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.PrototypingFolder.UI
{
  public class DragItems : MonoBehaviour
  {
    [NonSerialized]
    public Slot DraggingSlot;
    [NonSerialized]
    public Inventory DraggingInventory;
    
    private GameInput _gameInput;

    private void Inject(GameInput gameInput)
    {
      _gameInput = gameInput;
    }

    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
      
    }
  }
}