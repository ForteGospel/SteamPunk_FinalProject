using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlatform : MonoBehaviour
{
    [SerializeField]
    bool counterclockwise; // rotation left or right?

    [SerializeField]
    private float speed;

    private void Start()
    {
        if (counterclockwise)
            speed = -speed;
    }

    private void FixedUpdate()
    {
        if (counterclockwise)
        {
            speed = -speed;
            counterclockwise = false;
        }
            
        foreach (Transform child in transform)
            child.RotateAround(transform.position, transform.up, speed * Time.deltaTime);
        
    }


    private void OnTriggerEnter(Collider col)
    {
        col.transform.SetParent(this.transform, false);
    }

    private void OnTriggerExit(Collider col)
    {
        col.transform.parent = null;
    }
}
