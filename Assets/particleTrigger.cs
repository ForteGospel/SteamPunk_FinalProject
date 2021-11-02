using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTrigger : MonoBehaviour
{
    ParticleSystem ps;
    public GameObject colliderGO;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable ec))
        {
            ec.TakeRangeDamage();
        }
    }
}
