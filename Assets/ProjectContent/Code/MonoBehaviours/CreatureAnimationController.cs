using System;
using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using Zenject;
using static ProjectContent.Code.Csharps.Enums.ConstValues.Animation;

namespace ProjectContent.Code.MonoBehaviours
{
  [RequireComponent(typeof(Creature))]
  public class CreatureAnimationController : UnityEngine.MonoBehaviour
  {
    public Animator animator;
    public Creature Creature;

    protected MovementController _movementController;
    
    [Inject]
    private void Inject(MovementController movementController)
    {
      _movementController = movementController;
    }

    private void Start()
    {
      Creature.creatureStats.stats[typeof(Health)].OnValueChanged += UpdateHeath;
    }

    private void UpdateHeath(StatBase stat)
    {
      if (stat.Value <= 0)
      {
        animator.SetBool(IsDeath, true);
        return;
      }
      
      animator.SetBool(IsTakeDamage, true);
    }

    protected virtual void Update()
    {
      if (_movementController.Velocity.magnitude > 0f)
      {
        animator.SetBool(IsRunning, true);
        if (_movementController.Velocity.y > 0)
          animator.SetBool(IsRunUp, true);
        else if (_movementController.Velocity.y < 0)
          animator.SetBool(IsRunUp, false);
        if (_movementController.Velocity.x != 0)
          animator.SetBool(IsRunSide, true);
        else
          animator.SetBool(IsRunSide, false);
      }
      else
        animator.SetBool(IsRunning, false);
    }
  }
}