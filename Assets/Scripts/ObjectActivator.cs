using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectActivator : MonoBehaviour, IInteractable
{
    //[SerializeField]
    //GameObject[] activablesObjects;

    [SerializeField]
    UnityEvent unityEvent;

    [SerializeField]
    bool isLoot;
    public void Interact()
    {
        unityEvent.Invoke();

        if (isLoot)
            Destroy(gameObject);
    }
}
