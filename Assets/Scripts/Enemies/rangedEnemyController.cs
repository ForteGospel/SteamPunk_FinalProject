using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemyController : enemyController
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform shottingPosition;

    [SerializeField]
    float projectileForce = 100f;

    protected override void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            GameObject newProjectile = Instantiate(projectile, shottingPosition.position, shottingPosition.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);

            try
            {
                Destroy(newProjectile, 10f);
            }
            catch
            { }
        }
        
        base.AttackPlayer();        
    }
}
