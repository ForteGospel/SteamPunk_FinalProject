using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject[] activablesObjects;
    public void Interact()
    {
        Debug.Log("interacted");
        foreach (GameObject activable in activablesObjects)
            activable.GetComponent<IActivable>().Activate();
    }

}
