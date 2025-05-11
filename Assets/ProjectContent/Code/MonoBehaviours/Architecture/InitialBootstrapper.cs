using ProjectContent.Code.Csharps.Architecture;
using UnityEngine;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours.Architecture
{
  public class InitialBootstrapper : UnityEngine.MonoBehaviour
  {
    private ProjectContext _context;
    private DiContainer _container;

    public GameObject coroutineRunnerPrefab;

    private CoroutineRunner _coroutineRunner;
    private GameInput _gameInput;
    [Inject]
    private void Inject(GameInput input)
    {
      _gameInput = input;
    }
    private void Awake()
    {
      _context = ProjectContext.Instance;
      _container = _context.Container;

      _coroutineRunner = Instantiate(coroutineRunnerPrefab,_context.transform).GetComponent<CoroutineRunner>();
      _container.Bind<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle().NonLazy();
      
      _gameInput.Enable();
      
      Game game = new Game(_coroutineRunner);
      _container.Bind<Game>().FromInstance(game).AsSingle().NonLazy();
      
    }
  }
}