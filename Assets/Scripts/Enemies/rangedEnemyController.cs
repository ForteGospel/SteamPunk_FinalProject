using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedEnemyController : enemyController
{
    [SerializeField]
    GameObject projectile;

    protected override void AttackPlayer()
    {
        base.AttackPlayer();

        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 10f);
    }
}
