using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenStaffEffect : StateMachineBehaviour
{
    public int effectIndex;

    public LayerMask whatIsEnemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ThirdPersonController ctrl = animator.GetComponent<ThirdPersonController>();

        ctrl.meleeWeapon.effects[effectIndex].Activate();

        ctrl.meleeWeapon.ParticleSystem.SetActive(true);

        Collider[] results = new Collider [8];

        int contacts = Physics.OverlapBoxNonAlloc(ctrl.meleeWeapon.centerAttack.transform.position, ctrl.meleeWeapon.size, results, ctrl.transform.rotation, whatIsEnemy);

        for (int i = 0; i < contacts; ++i)
        {
            results[i].gameObject.GetComponent<enemyController>().TakeDamage(ctrl.transform.position);
        }
    }
}
