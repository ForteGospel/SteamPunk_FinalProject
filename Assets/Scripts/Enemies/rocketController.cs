using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketController : MonoBehaviour
{
    [SerializeField]
    LayerMask whatToCollideWith;

    public GameObject currDestination;

    float TTL = 0;

    private void Update()
    {
        TTL += Time.deltaTime;

        if (TTL > 10f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (whatToCollideWith == (whatToCollideWith | (1 << collision.gameObject.layer)))
        {
            if (collision.transform.TryGetComponent<bigBossEnemy>(out bigBossEnemy enemy))
                enemy.realDamage();
            else if (collision.transform.TryGetComponent<StarterAssets.ThirdPersonController>(out StarterAssets.ThirdPersonController player))
                player.getHit(20f);
            else if (collision.transform.TryGetComponent<rangedEnemyController>(out rangedEnemyController rangeEnemy))
                rangeEnemy.TakeDamage(Vector3.zero);

                Destroy(gameObject);
        }
    }

    public void changeDestination()
    {
        Quaternion newRotation = Quaternion.Inverse(transform.rotation);
        GameObject newMissile = Instantiate(gameObject, transform.position, newRotation);

        Vector3 newDirection = (currDestination.transform.position - transform.position).normalized;

        newMissile.GetComponent<Rigidbody>().AddForce(newDirection * 5, ForceMode.Impulse);
        newMissile.GetComponent<Rigidbody>().AddTorque(newDirection * 10, ForceMode.Impulse);

        Destroy(gameObject);

    }
}
