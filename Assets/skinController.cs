using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinController : MonoBehaviour
{

    [SerializeField]
    floatScriptableObject health;

    [SerializeField]
    Material[] materials;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.value > 70)
            gameObject.GetComponent<SkinnedMeshRenderer>().material = materials[0];
        else if (health.value > 30)
            gameObject.GetComponent<SkinnedMeshRenderer>().material = materials[1];
        else
            gameObject.GetComponent<SkinnedMeshRenderer>().material = materials[2];
    }
}
