using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereEnemyController : MonoBehaviour, IDamagable
{
    // Start is called before the first frame update
    [SerializeField]
    float force = 10;

    [SerializeField]
    LayerMask whatToCollideWith;

    [SerializeField]
    GameObject explossionEffect;
    public void TakeDamage(Vector3 damage)
    {
            Vector3 direction = (transform.position - damage).normalized;
            direction = new Vector3(direction.x, 0.4f, direction.z);
            gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);            
    }

    public void TakeRangeDamage()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (whatToCollideWith == (whatToCollideWith | (1 << collision.gameObject.layer)))
        {
            if (collision.transform.TryGetComponent<bigBossEnemy>(out bigBossEnemy enemy))
                enemy.realDamage();

            Instantiate(explossionEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
