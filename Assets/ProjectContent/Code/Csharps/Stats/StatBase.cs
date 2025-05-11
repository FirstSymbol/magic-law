using System;
using UnityEngine;

namespace ProjectContent.Code.Csharps.Stats
{
  [Serializable]
  public abstract class StatBase
  {
    public Action<StatBase> OnValueChanged {get; set;}
    
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public float MaxValue { get; private set; }
    public bool Active = true;

    public virtual void SetValue(float value)   
    {
      Value = value;
      OnValueChanged?.Invoke(this);
    }

    public virtual void SubstractValue(float value)
    {
      Value -= value;
      if (Value < 0)
        Value = 0;
      
      OnValueChanged?.Invoke(this);
    }

    public virtual void AddValue(float value)
    {
      Value += value;

      if (Value > MaxValue && MaxValue > 0) 
        Value = MaxValue;
      
      OnValueChanged?.Invoke(this);
    }
  
  }
}