using System.Collections;
using ProjectContent.Code.Csharps.Architecture.StateMachine;
using ProjectContent.Code.MonoBehaviours.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using AsyncOperation = UnityEngine.AsyncOperation;

namespace ProjectContent.Code.Csharps.Architecture.States
{
  public class LoadSceneState : PayloadState<string>
  {
    private CoroutineRunner _coroutineRunner;
    private LoadingScreen _loadingScreen;

    [Inject]
    private void Inject()
    {
      Debug.Log("Inject");
    }
    
    public LoadSceneState(StateMachineBase stateMachine,CoroutineRunner coroutineRunner, LoadingScreen loadingScreen) : base(stateMachine)
    {
      _coroutineRunner = coroutineRunner;
      _loadingScreen = loadingScreen;
    }

    public override void Enter(string sceneName)
    {
      Debug.Log("[LoadSceneState] Enter");
      
      _coroutineRunner.StartCoroutine(SceneLoading(sceneName));
      
    }

    IEnumerator SceneLoading(string sceneName)
    {
      Debug.Log($"Loading scene {sceneName}...");
      
      AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
      
      if (sceneAsync == null)
      {
        Debug.LogError($"Failed to load scene: {sceneName}");
        yield break;
      }
      _loadingScreen.Show();
      yield return new WaitForSeconds(2f);
      LoadingScreen loadingScreen;
      sceneAsync.completed += (AsyncOperation _) =>
      {
        Debug.Log($"Scene {sceneName} successfully loaded!");
        StateMachine.Enter<GameloopState>();
        _loadingScreen.Hide();
      };
      
      while (!sceneAsync.isDone)
      {
        yield return null;
      }
    }

    public override void Exit()
    {
      Debug.Log("[LoadSceneState] Exit");
    }
  }
}