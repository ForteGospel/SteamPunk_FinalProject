using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cogColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform; // keep the player as a child, to rotate him with the platform rotation
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null; // remove the player as a child
    }
}
