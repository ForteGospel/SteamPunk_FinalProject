using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FlyingSteam : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out StarterAssets.ThirdPersonController TPC))
        {
            TPC.inSteam = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent(out StarterAssets.ThirdPersonController TPC))
        {
            TPC.inSteam = false;
        }
    }
}
