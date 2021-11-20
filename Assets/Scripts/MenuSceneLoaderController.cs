using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneLoaderController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene (int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
