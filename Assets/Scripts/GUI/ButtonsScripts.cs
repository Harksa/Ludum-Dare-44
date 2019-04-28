using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScripts : MonoBehaviour
{
    public void StartGame() {
        GameManager.StartGame();
        SceneManager.LoadScene(1);
    }

    public void ReturnToMain() {
        SceneManager.LoadScene(0);
    }
    
    public void ExitGame() {
        Application.Quit();
    }
}
