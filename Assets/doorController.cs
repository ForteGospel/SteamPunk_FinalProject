using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    bool doorStatus = false;

    public void changeStatus()
    {
        doorStatus = !doorStatus;

        gameObject.GetComponent<Animator>().SetBool("isOpen", doorStatus);
    }
}
