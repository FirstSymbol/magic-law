using System.Collections;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class Movement : MonoBehaviour
  {
    public Rigidbody2D Rigidbody;
    public Creature Creature;
    public SpriteRenderer SpriteRenderer;
    public float RunCoastPerSec = 1f;
    [ReadOnly] public float SpeedModifier = 1f;
    [ReadOnly] public int DashCoast = 2; 
    public float DashDistance = 1f;
    public float DashTime = 0.5f;
    private MovementController _movementController;
    private GameInput _gameInput;
    private Coroutine _coroutine;
    private bool _isDashing;
    private const float RunModifier = 1.5f;
    private readonly WaitForSeconds _wait = new WaitForSeconds(1f);

    [Inject]
    private void Inject(MovementController movementController, GameInput gameInput)
    {
      _movementController = movementController;
      _gameInput = gameInput;
    }
    
    private void Awake()
    {
      Creature = GetComponent<Creature>();
    }

    private void Start()
    {
      Init();
    }

    private void Init()
    {
      _gameInput.Player.Run.performed += StartRun;
      _gameInput.Player.Run.canceled += StopRun;
      _gameInput.Player.Dash.performed += Dash;
    }

    private void OnDestroy()
    {
      _gameInput.Player.Run.performed -= StartRun;
      _gameInput.Player.Run.canceled -= StopRun;
      _gameInput.Player.Dash.performed -= Dash;
    }

    private void Update()
    {
      FlifByX();
      Move();
    }

    private void Move()
    {
      if (_isDashing)
      {
        return;
      }
      Rigidbody.linearVelocity = _movementController.Velocity * Creature.CreatureStats.Speed.Value * SpeedModifier;
    }

    private void Dash(InputAction.CallbackContext obj)
    {
      if(_isDashing || 
         _movementController.Velocity.magnitude == 0 || 
         Creature.CreatureStats.Stamina.Value < RunCoastPerSec) return;
        StartCoroutine(Dashing());
    }

    private IEnumerator Dashing()
    {
      _isDashing = true;
      float dashPower;
      float timer = 0f;
      Vector2 dashVector = _movementController.Velocity;
      Creature.CreatureStats.Stamina.SubstractValue(DashCoast);
      
      while (timer < DashTime)
      {
        dashPower = DashDistance / DashTime;
        Rigidbody.linearVelocity = dashVector * dashPower;
        timer += Time.deltaTime;
        yield return null;
      }
      _isDashing = false;
    }

    private void StartRun(InputAction.CallbackContext obj)
    {
      if (Creature.CreatureStats.Stamina.Value > RunCoastPerSec)
      {
        _coroutine = StartCoroutine(RunCoroutine());
      }
      
    }

    private void StopRun(InputAction.CallbackContext obj)
    {
      if (_coroutine != null) StopCoroutine(_coroutine);
      SpeedModifier = 1f;
    }

    private IEnumerator RunCoroutine()
    {
      while (Creature.CreatureStats.Stamina.Value > RunCoastPerSec)
      {
        Creature.CreatureStats.Stamina.SubstractValue(RunCoastPerSec);
        SpeedModifier = RunModifier;
        yield return _wait;
      }
      
      SpeedModifier = 1f;
      
    }

    private void FlifByX()
    {
      if (_movementController.Velocity.x < 0 && SpriteRenderer != null)
      {
        SpriteRenderer.flipX = true;
      }
      else if (_movementController.Velocity.x >= 0 && SpriteRenderer != null)
      {
        SpriteRenderer.flipX = false;
      }
    }
  }
}