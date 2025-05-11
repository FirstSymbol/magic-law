using UnityEngine;
using static ProjectContent.Code.Csharps.Enums.ConstValues.Animation;
namespace ProjectContent.Code.AnimatorStateMachineBehaviours
{
    public class TakeDamageBehaviour : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(IsTakeDamage, false);
        }
    }
}
