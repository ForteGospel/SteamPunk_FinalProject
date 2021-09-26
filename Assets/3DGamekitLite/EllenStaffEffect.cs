using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenStaffEffect : StateMachineBehaviour
{
    public int effectIndex;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ThirdPersonController ctrl = animator.GetComponent<ThirdPersonController>();

        ctrl.meleeWeapon.effects[effectIndex].Activate();

        ctrl.meleeWeapon.ParticleSystem.SetActive(true);
    }

}
