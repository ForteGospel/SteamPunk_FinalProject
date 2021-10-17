using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public TimeEffect[] effects;
    public GameObject ParticleSystem;

    public Vector3 center; 
    public Vector3 size;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(center, size);
    }
}
