using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneDeathController : MonoBehaviour
{
    [SerializeField]
    GameObject explossionEffect;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        gameObject.GetComponent<Rigidbody>().AddTorque((Vector3.up + Vector3.right) * 50, ForceMode.Impulse);
        Invoke(nameof(explode), 3f);
    }

    void explode()
    {
        Instantiate(explossionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
