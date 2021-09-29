using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogController : MonoBehaviour, IInteractable
{
    [SerializeField]
    DialogObjects dialog;
    public void Interact()
    {
        DialogUIController.instance.showDialog(dialog);
    }

}
