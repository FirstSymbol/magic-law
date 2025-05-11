using System;
using System.Collections;
using ProjectContent.Code.MonoBehaviours.Creatures;
using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class Movement : UnityEngine.MonoBehaviour
  {
    private MovementController _movementController;
    public Rigidbody2D Rigidbody;
    public Creature _creature;
    public SpriteRenderer spriteRenderer;
    public float RunCoastPerSec = 1f;
    private readonly float RunModifier = 1.5f;
    [ReadOnly] public float SpeedModifier = 1f;
    [ReadOnly] public int DashCoast = 2; 
    public float DashDistance = 1f;
    public float DashTime = 0.5f;
    private GameInput _gameInput;
    private WaitForSeconds _wait = new WaitForSeconds(1f);
    private Coroutine _coroutine;
    private bool _isDashing;

    [Inject]
    private void Inject(MovementController movementController, GameInput gameInput)
    {
      _movementController = movementController;
      _gameInput = gameInput;
    }
    
    private void Awake()
    {
      _creature = GetComponent<Creature>();
    }

    private void OnEnable()
    {
      _gameInput.Player.Run.performed += StartRun;
      _gameInput.Player.Run.canceled += StopRun;
      _gameInput.Player.Dash.performed += Dash;
    }

    private void OnDisable()
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
      Rigidbody.linearVelocity = _movementController.Velocity * _creature.creatureStats.Speed.Value * SpeedModifier;
    }

    private void Dash(InputAction.CallbackContext obj)
    {
      if((_isDashing || _movementController.Velocity.magnitude == 0) && _creature.creatureStats.Stamina.Value > RunCoastPerSec) return;
        StartCoroutine(Dashing());
    }

    private IEnumerator Dashing()
    {
      _isDashing = true;
      float dashPower;
      float timer = 0f;
      Vector2 dashVector = _movementController.Velocity;
      _creature.creatureStats.Stamina.SubstractValue(DashCoast);
      
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
      if (_creature.creatureStats.Stamina.Value > RunCoastPerSec)
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
      while (_creature.creatureStats.Stamina.Value > RunCoastPerSec)
      {
        _creature.creatureStats.Stamina.SubstractValue(RunCoastPerSec);
        SpeedModifier = RunModifier;
        yield return _wait;
      }
      
      SpeedModifier = 1f;
      
    }

    private void FlifByX()
    {
      if (_movementController.Velocity.x < 0 && spriteRenderer != null)
      {
        spriteRenderer.flipX = true;
      }
      else if (_movementController.Velocity.x >= 0 && spriteRenderer != null)
      {
        spriteRenderer.flipX = false;
      }
    }
  }
}