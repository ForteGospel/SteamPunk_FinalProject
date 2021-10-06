using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SteamObjectController : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    floatScriptableObject floatObject;

    [SerializeField]
    float speed;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if(playerLayer == (playerLayer | (1 <<other.gameObject.layer)))
        {
            if ((floatObject.value <= 100f && speed > 0) || (floatObject.value >= 0f && speed < 0))
                floatObject.value += speed * Time.deltaTime;
        }
    }
}
