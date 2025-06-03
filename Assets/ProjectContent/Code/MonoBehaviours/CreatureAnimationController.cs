using ProjectContent.Code.Csharps.Stats;
using ProjectContent.Code.MonoBehaviours.Creatures;
using UnityEngine;
using Zenject;
using static ProjectContent.Code.Csharps.Enums.ConstValues.Animation;

namespace ProjectContent.Code.MonoBehaviours
{
  /// <summary>
  ///   Базовый класс управления анимациями существа
  /// </summary>
  [RequireComponent(typeof(Creature))]
  public class CreatureAnimationController : MonoBehaviour
  {
    public Animator Animator;
    public Creature Creature;

    protected MovementController _movementController;

    private void Start()
    {
      Creature.CreatureStats.stats[typeof(Health)].OnValueChanged += UpdateHeath;
    }

    protected virtual void Update()
    {
      if (_movementController.Velocity.magnitude > 0f)
      {
        Animator.SetBool(IsRunning, true);
        if (_movementController.Velocity.y > 0)
          Animator.SetBool(IsRunUp, true);
        else if (_movementController.Velocity.y < 0)
          Animator.SetBool(IsRunUp, false);
        if (_movementController.Velocity.x != 0)
          Animator.SetBool(IsRunSide, true);
        else
          Animator.SetBool(IsRunSide, false);
      }
      else
      {
        Animator.SetBool(IsRunning, false);
      }
    }

    [Inject]
    private void Inject(MovementController movementController)
    {
      _movementController = movementController;
    }

    private void UpdateHeath(StatBase stat)
    {
      if (stat.Value <= 0)
      {
        Animator.SetBool(IsDeath, true);
        return;
      }

      Animator.SetBool(IsTakeDamage, true);
    }
  }
}