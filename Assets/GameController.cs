using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    floatScriptableObject health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.value <= 0f)
        {
            //PauseGameController.instance.pauseGame();
            gameOverUI.SetActive(true);
            Invoke(nameof(restartLevel), 5f);
        }
    }

    private void restartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
