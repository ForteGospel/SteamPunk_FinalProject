using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject frontThrusters;
    [SerializeField] GameObject backThrusters;
    [SerializeField] GameObject leftThrusters;
    [SerializeField] GameObject rightThrusters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlThrusters();

        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime, ForceMode2D.Impulse);
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed * -Input.GetAxis("Horizontal"));
    }

    void controlThrusters()
    {
        if (Input.GetAxis("Vertical") > 0)
            backThrusters.SetActive(true);
        else if (Input.GetAxis("Vertical") < 0)
            frontThrusters.SetActive(true);
        else
        {
            frontThrusters.SetActive(false);
            backThrusters.SetActive(false);
        }

        if (Input.GetAxis("Horizontal") > 0)
            leftThrusters.SetActive(true);
        else if (Input.GetAxis("Horizontal") < 0)
            rightThrusters.SetActive(true);
        else
        {
            leftThrusters.SetActive(false);
            rightThrusters.SetActive(false);
        }
    }
}
