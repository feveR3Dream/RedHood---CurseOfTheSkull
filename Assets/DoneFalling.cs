using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneFalling : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isStartFalling", true);
    }
}
