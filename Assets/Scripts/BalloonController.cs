using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField]
    Vector3 center;

    [SerializeField]
    float speed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center, transform.up, speed * Time.deltaTime);
    }
}
