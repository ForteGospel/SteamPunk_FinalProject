using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class graveStoneController : MonoBehaviour, IInteractable
{
    [SerializeField]
    UnityEvent graveStoneEvent;

    [SerializeField]
    DialogObjects dialog;

    public void Interact()
    {
        DialogUIController.instance.showTempDialog(dialog);
        graveStoneEvent.Invoke();
    }
}
