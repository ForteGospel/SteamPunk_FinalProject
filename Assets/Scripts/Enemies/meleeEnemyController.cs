using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemyController : enemyController
{
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        Collider[] results = new Collider[8];

        int contacts = Physics.OverlapBoxNonAlloc(centerAttack.transform.position, size, results, transform.rotation, whatIsPlayer);

        for (int i = 0; i < contacts; ++i)
        {
            results[i].gameObject.GetComponent<StarterAssets.ThirdPersonController>().getHit(hitDamage);
        }
    }
}
