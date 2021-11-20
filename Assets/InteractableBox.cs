using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableBox : MonoBehaviour
{
    [SerializeField]
    Text uiText;

    [SerializeField]
    string interactableString;

    [SerializeField]
    LayerMask whatIsInteractable;

    private void OnTriggerEnter(Collider other)
    {
        if (whatIsInteractable == (whatIsInteractable | (1 << other.gameObject.layer)))
            uiText.text = interactableString;
    }

    private void OnTriggerExit(Collider other)
    {
        if (whatIsInteractable == (whatIsInteractable | (1 << other.gameObject.layer)))
            uiText.text = "";
    }
}
