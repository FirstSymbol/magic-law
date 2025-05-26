using System.Collections;
using TMPro;
using TriInspector;
using UnityEngine;

namespace ProjectContent.Code.MonoBehaviours.UI
{
  public class LoadingScreen : MonoBehaviour
  {
    [SerializeField] private string _loadingText = "LOADING";
    public TextMeshProUGUI LoadingComp;
    public Canvas LoadingCanvas;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.33f);

    public void Show()
    {
      LoadingCanvas.gameObject.SetActive(true);
      StartCoroutine(LoadingScreenRoutine());
    }

    public void Hide()
    {
      LoadingCanvas.gameObject.SetActive(false);
    }

    private IEnumerator LoadingScreenRoutine()
    {
      LoadingComp.text = _loadingText;
      while (LoadingCanvas.gameObject.activeSelf)
      {
        if (LoadingComp.text.Length < _loadingText.Length + 3)
          LoadingComp.text += '.';
        else
          LoadingComp.text = _loadingText;
        yield return _waitForSeconds;
      }
    }
  }
}