using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIController : MonoBehaviour
{
    public static DialogUIController instance { get; private set; }

    [SerializeField]
    Text UIText;

    [SerializeField]
    Text buttonText;

    [SerializeField]
    Image image;

    DialogObjects currentDialog;

    int currentTextLineNumber = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hideUI();
    }


    public void showDialog (DialogObjects dialog)
    {
        showUI();
        currentDialog = dialog;
        updateDialog();
    }

    public void updateDialog()
    {
        if (!(currentTextLineNumber == currentDialog.dialog.Length))
        {
            if (currentTextLineNumber >= currentDialog.dialog.Length - 1)
                buttonText.text = "Finish";
            else
                buttonText.text = "Next";
            image.sprite = currentDialog.image;

            UIText.text = currentDialog.dialog[currentTextLineNumber];

            currentTextLineNumber++;
        }
        else
        {
            currentTextLineNumber = 0;
            hideUI();
        }

    }

    private void showUI()
    {
        gameObject.SetActive(true);
        PauseGameController.instance.pauseGame();
        
    }

    private void hideUI()
    {
        gameObject.SetActive(false);
        PauseGameController.instance.unPauseGame();
    }
}
