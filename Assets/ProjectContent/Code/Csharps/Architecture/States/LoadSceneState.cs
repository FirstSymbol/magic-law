using System.Collections;
using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class LoadSceneState : PayloadState<string>
  {
    private readonly CoroutineRunner _coroutineRunner;
    private readonly LoadingScreen _loadingScreen;

    public LoadSceneState(StateMachineBase stateMachine, CoroutineRunner coroutineRunner, LoadingScreen loadingScreen) :
      base(stateMachine)
    {
      _coroutineRunner = coroutineRunner;
      _loadingScreen = loadingScreen;
    }

    [Inject]
    private void Inject()
    {
      Debug.Log("Inject");
    }

    public override void Enter(string sceneName)
    {
      Debug.Log("[LoadSceneState] Enter");

      _coroutineRunner.StartCoroutine(SceneLoading(sceneName));
    }

    private IEnumerator SceneLoading(string sceneName)
    {
      Debug.Log($"Loading scene {sceneName}...");

      var sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

      if (sceneAsync == null)
      {
        Debug.LogError($"Failed to load scene: {sceneName}");
        yield break;
      }

      _loadingScreen.Show();
      LoadingScreen loadingScreen;
      sceneAsync.completed += _ =>
      {
        Debug.Log($"Scene {sceneName} successfully loaded!");
        _loadingScreen.Hide();
      };

      while (!sceneAsync.isDone) yield return null;
    }

    public override void Exit()
    {
      Debug.Log("[LoadSceneState] Exit");
    }
  }
}