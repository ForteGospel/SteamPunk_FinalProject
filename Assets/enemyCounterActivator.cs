using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyCounterActivator : MonoBehaviour
{
    [SerializeField]
    UnityEvent unityevent;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            unityevent.Invoke();
        }
    }
}
