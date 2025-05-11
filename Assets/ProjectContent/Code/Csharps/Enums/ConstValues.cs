using UnityEngine;

namespace ProjectContent.Code.Csharps.Enums
{
  public static class ConstValues
  { 
    public static class Architecture
    {
      public const string InitialScene = "InitialScene";
      public const string GameloopScene = "Gameloop";
    }
    public static class Animation
    {
      #region CreatureAnimations
      
      public static readonly int IsRunning = Animator.StringToHash("isRunning");
      public static readonly int IsRunSide = Animator.StringToHash("isRunSide");
      public static readonly int IsRunUp = Animator.StringToHash("isRunUp");
      public static readonly int IsTakeDamage = Animator.StringToHash("isTakeDamage");
      public static readonly int IsDeath = Animator.StringToHash("isDeath");

      #endregion
      
      #region ItemAnimations
      
      public static readonly int IsUse = Animator.StringToHash("isUse");
      public static readonly int IsAlternativeUse = Animator.StringToHash("isAlternativeUse");
      
      #endregion
      
    }
  }
}