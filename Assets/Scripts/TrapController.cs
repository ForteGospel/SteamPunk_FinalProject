using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    float damageAmount = 20f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out StarterAssets.ThirdPersonController TPC))
        {
            TPC.getHit(damageAmount);
        }
    }
}
