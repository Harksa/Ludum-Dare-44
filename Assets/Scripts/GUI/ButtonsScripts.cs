using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScripts : MonoBehaviour
{
    [SerializeField] private GameObject _loadingText = null;

    private void Start() {
        _loadingText.SetActive(false);
    }

    public void StartGame() {
        GameManager.StartGame();
        StartCoroutine(SceneLoader(1));
    }

    public void ResumeGame(){
        GameManager.State = GameManager.STATE.Running;
    }

    public void ReturnToMain() {
        StartCoroutine(SceneLoader(0));
    }
    
    public void ExitGame() {
        Application.Quit();
    }

    private IEnumerator SceneLoader(int scene) {

        _loadingText.SetActive(true);

        var async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        yield return async.isDone;

        async.allowSceneActivation = true;
    }
}
