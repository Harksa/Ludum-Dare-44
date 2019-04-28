using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameGUI : MonoBehaviour
{

    [Header("INGAME")]
    [SerializeField] private Slider _lifeBar = null;
    [SerializeField] private TextMeshProUGUI _lifeText = null;
    [SerializeField] private TextMeshProUGUI _waveText = null;
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    [SerializeField] private TextMeshProUGUI _remainingText = null;
    [SerializeField] private TextMeshProUGUI _highScoreText = null;

    [Header("SHOP")]
    [SerializeField] private GameObject _shopCanvas = null;


    // Start is called before the first frame update
    void Start()
    {
        _shopCanvas.SetActive(false);

        GameManager.HealthChanged += delegate(int health) {
            _lifeBar.value = health;
            _lifeText.text = $"{health} / {GameManager.CurrentMaxHealth}";
        };

        GameManager.MaxhHealthChanged += delegate(int maxHealth) {
            _lifeBar.maxValue = maxHealth;
            _lifeText.text = $"{GameManager.Health} / {maxHealth}";
        };

        GameManager.OnStartWave += delegate() {
            _waveText.text = $"Wave : {GameManager.CurrentWave}";
        };

        GameManager.ScoreChanged += delegate(int score) {
            _scoreText.text = $"Score : {score}";
        };

        GameManager.HighScoreChanged += delegate(int highscore) {
            _highScoreText.text = $"HighScore : {highscore}";
        };

        GameManager.EnemyRemainingChanged += delegate(int remaining) {
            _remainingText.text = $"Remains : {remaining}";
        };

        _highScoreText.text = $"HighScore : {PlayerPrefs.GetInt("HighScore", 0)}";

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
