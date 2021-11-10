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

    [SerializeField]
    GameObject panel;

    [SerializeField]
    Text nameText;

    DialogObjects currentDialog;

    float secondsForTempUI = 0;

    int currentTextLineNumber = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hideUI();
    }

    private void Update()
    {
        checkForTempUI();
    }


    public void showDialog (DialogObjects dialog)
    {
        currentTextLineNumber = 0;
        showUI();
        currentDialog = dialog;
        updateDialog();
    }

    public void showTempDialog(DialogObjects dialog)
    {
        currentTextLineNumber = 0;
        currentDialog = dialog;
        updateDialog();
        secondsForTempUI = 2f;       
    }

    public void updateDialog()
    {
        if (currentTextLineNumber == currentDialog.dialog.Length)
        {
            hideUI();
        }
        else
        {
            if (currentTextLineNumber >= currentDialog.dialog.Length - 1)
                buttonText.text = "Finish";
            else
                buttonText.text = "Next";
            image.sprite = currentDialog.image;

            UIText.text = currentDialog.dialog[currentTextLineNumber];

            nameText.text = currentDialog.name;

            currentTextLineNumber++;
        }

    }

    private void showUI()
    {
        panel.SetActive(true);
        secondsForTempUI = 0.1f;
        PauseGameController.instance.pauseGame();
        
    }

    private void checkForTempUI()
    {
        if (secondsForTempUI > 0f)
        {
            panel.SetActive(true);
            secondsForTempUI -= Time.deltaTime;
        }
        else
            panel.SetActive(false);
    }

    private void hideUI()
    {
        panel.SetActive(false);
        PauseGameController.instance.unPauseGame();
    }
}
