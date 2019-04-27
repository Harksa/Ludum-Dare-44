using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour
{

    [Header("SHOP")]
    [SerializeField] private GameObject _shopCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _shopCanvas.SetActive(false);

        GameManager.StateChanged += OnStateChanged;

    }

    public void LaunchNextWave() {
        GameManager.StartNextWave();
    }

    private void OnStateChanged(GameManager.STATE state) {
        if(state == GameManager.STATE.InShop) {
            _shopCanvas.SetActive(true);
        } else {
            _shopCanvas.SetActive(false);
        }
    }

}
