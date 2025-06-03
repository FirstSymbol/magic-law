using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectContent.Code.Csharps.Enums
{
  /// <summary>
  ///   Все нужные константные значения для доступа из любой точки системы.
  /// </summary>
  public static class ConstValues
  {
    /// <summary>
    ///   Наименования сцен
    /// </summary>
    public static class Scenes
    {
      public const string MainMenu = "MainMenu";
      public const string InitialScene = "InitialScene";
      public const string GameloopScene = "Gameloop";
    }

    public static class Architecture
    {
    }

    /// <summary>
    ///   Название переменных аниматоров.
    /// </summary>
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

    /// <summary>
    ///   Ссылки для получения адресуемых ассетов.
    /// </summary>
    public static class Addressables
    {
      public static AssetReferenceGameObject PlayerPrefab = new("PlayerPrefab");
    }
  }
}