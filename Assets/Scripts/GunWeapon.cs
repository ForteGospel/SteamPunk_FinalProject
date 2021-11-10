using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    [SerializeField]
    float timer = 2f;

    [SerializeField]
    GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void fireGun()
    {
        effect.SetActive(true);
    }

    public void holdGun()
    {
        effect.SetActive(false);
    }
}
