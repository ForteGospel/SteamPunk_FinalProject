using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //other.GetComponent<StarterAssets.ThirdPersonController>()._verticalVelocity = +2f; 
        other.GetComponent<CharacterController>().Move(transform.forward * 1 * Time.deltaTime);
    }
}
