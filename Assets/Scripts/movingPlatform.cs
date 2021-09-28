using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour, IActivable
{
    private Vector3 startingPoint;
    private Vector3 destinationPoint;
    private Vector3 nextDestinationPoint;

    [SerializeField]
    float platformSpeed = 1;

    [SerializeField]
    Vector3 Direction = new Vector3(1,1,1);

    [SerializeField]
    float multiplaier = 2;

    [SerializeField]
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
        destinationPoint = startingPoint + Direction * multiplaier;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            moveTowardsDestination();
    }

    protected virtual void moveTowardsDestination()
    {
        if (Vector3.Distance(transform.position, startingPoint) < 0.5f)
        {
            nextDestinationPoint = destinationPoint;
        }
        if (Vector3.Distance(transform.position, destinationPoint) < 0.5f)
        {
            nextDestinationPoint = startingPoint;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextDestinationPoint, platformSpeed / 100);
    }

    private void OnDrawGizmos()
    {
        if (isActive)
            Gizmos.DrawLine(transform.position, transform.position + Direction * multiplaier);
    }

    public void Activate()
    {
        isActive = !isActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.transform.SetParent(null);
    }
}
