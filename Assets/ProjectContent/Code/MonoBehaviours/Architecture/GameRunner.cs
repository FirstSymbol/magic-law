using ProjectContent.Code.Csharps.Enums;
using UnityEngine.SceneManagement;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  public class GameRunner : UnityEngine.MonoBehaviour
  {
    private void Awake()
    {
      if (SceneManager.GetActiveScene().name != ConstValues.Architecture.InitialScene) 
        SceneManager.LoadScene(ConstValues.Architecture.InitialScene);
    }
  }
}