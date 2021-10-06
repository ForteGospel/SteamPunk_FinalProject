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
    public void Interact()
    {
        //Debug.Log("interacted");
        //foreach (GameObject activable in activablesObjects)
        //    activable.GetComponent<IActivable>().Activate();

        unityEvent.Invoke();
    }

}
