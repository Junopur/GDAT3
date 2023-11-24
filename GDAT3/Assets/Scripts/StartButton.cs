using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Build number of scene to start when button is pressed
    public int gameStartScene;

    // 
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
    
}
