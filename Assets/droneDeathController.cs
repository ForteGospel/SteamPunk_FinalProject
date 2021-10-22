using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneDeathController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        gameObject.GetComponent<Rigidbody>().AddTorque((Vector3.up + Vector3.right) * 50, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
