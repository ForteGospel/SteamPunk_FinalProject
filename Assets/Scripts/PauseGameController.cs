using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{

    public static PauseGameController instance { get; private set; }

    public bool isGamePaused = false;

    private void Awake()
    {
        instance = this;
        unPauseGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void unPauseGame()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        isGamePaused = false;
    }
}
