using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingsController : MonoBehaviour
{

    private void OnEnable()
    {
        gameObject.GetComponent<Animator>().SetTrigger("enabled");
        Debug.Log("enabled");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
