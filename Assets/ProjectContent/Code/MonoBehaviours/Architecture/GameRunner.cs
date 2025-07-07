using ProjectContent.Code.Csharps.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  /// <summary>
  ///   Класс для начала игры, который всегда запускает со сцены инициализации.
  /// </summary>
  public class GameRunner : MonoBehaviour
  {
    private void Awake()
    {
      if (SceneManager.GetActiveScene().name != ConstValues.Scenes.InitialScene)
        SceneManager.LoadScene(ConstValues.Scenes.InitialScene);
    }
  }
}