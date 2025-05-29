using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  public class InitialBootstrapper : MonoBehaviour
  {
    private ProjectContext _context;
    [Inject] private LoadingScreen _loadingScreen;
    

    public GameObject coroutineRunnerPrefab;

    private CoroutineRunner _coroutineRunner;
    private GameInput _gameInput;
    [Inject]
    private void Inject(GameInput input, CoroutineRunner runner)
    {
      _gameInput = input;
      _coroutineRunner = runner;
    }
    private void Awake()
    {
      _gameInput.Enable();
      
      Game.Initialize(_coroutineRunner, _loadingScreen);
    }
  }
}