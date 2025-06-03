using ProjectContent.Code.Csharps.Architecture;
using ProjectContent.Code.MonoBehaviours.UI;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  /// <summary>
  ///   Скрипт инициализации, который запускается в сцене инициализации.
  /// </summary>
  public class InitialBootstrapper : MonoBehaviour
  {
    private ProjectContext _context;


    private CoroutineRunner _coroutineRunner;
    private GameInput _gameInput;
    [Inject] private LoadingScreen _loadingScreen;

    private void Awake()
    {
      _gameInput.Enable();

      Game.Initialize(_coroutineRunner, _loadingScreen);
    }

    [Inject]
    private void Inject(GameInput input, CoroutineRunner runner)
    {
      _gameInput = input;
      _coroutineRunner = runner;
    }
  }
}