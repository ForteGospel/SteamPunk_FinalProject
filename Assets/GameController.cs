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
            Invoke(nameof(showGameOver), 2f);
        }
    }

    private void showGameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        gameOverUI.SetActive(true);
    }
}
