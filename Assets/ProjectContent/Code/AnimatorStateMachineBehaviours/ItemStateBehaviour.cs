using UnityEngine;
using static ProjectContent.Code.Csharps.Enums.ConstValues.Animation;

namespace ProjectContent.Code.AnimatorStateMachineBehaviours
{
  public class ItemStateBehaviour : StateMachineBehaviour
  {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      animator.SetBool(IsUse, false);
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
    }
  }
}