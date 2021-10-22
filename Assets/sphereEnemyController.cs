using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereEnemyController : enemyController
{
    // Start is called before the first frame update
    [SerializeField]
    float force = 10;

    [SerializeField]
    LayerMask whatToCollideWith;

    private void Update()
    {
        
    }
    public override void TakeDamage(Vector3 damage)
    {
            Vector3 direction = (transform.position - damage).normalized;
            direction = new Vector3(direction.x, 0.4f, direction.z);
            gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (whatToCollideWith == (whatToCollideWith | (1 << collision.gameObject.layer)))
        {
            Debug.Log("hit");
        }
    }
}
