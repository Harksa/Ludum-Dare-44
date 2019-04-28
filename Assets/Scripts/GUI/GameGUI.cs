using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    [SerializeField] private TextMeshProUGUI _damageCardText = null;
    [SerializeField] private TextMeshProUGUI _fireRateCardText = null;
    [SerializeField] private TextMeshProUGUI _speedCardText = null;
    [SerializeField] private TextMeshProUGUI _healthCardText = null;

    [Header("GAMEOVER")]
    [SerializeField] private GameObject _gameOverCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        _shopCanvas.SetActive(false);
        _gameOverCanvas.SetActive(false);

        GameManager.HealthChanged += delegate(int health) {
            _lifeBar.value = health;
            _lifeText.text = $"{health} / {GameManager.CurrentMaxHealth}";
        };

        GameManager.MaxhHealthChanged += delegate(int maxHealth) {
            _lifeBar.maxValue = maxHealth;
            _lifeText.text = $"{GameManager.PlayerHealth} / {maxHealth}";
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

        string baseBonusText = $"Loose {GameManager.HPLostForBonus} HP and get";

        NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        _damageCardText.text = $"{baseBonusText} {GameManager.PlayerIncreaseDamages} bonus damages";
        _fireRateCardText.text = $"{baseBonusText} {GameManager.PlayerIncreaseFireRate.ToString(nfi)} bonus fire rate";
        _speedCardText.text = $"{baseBonusText} {GameManager.PlayerIncreaseSpeed.ToString(nfi)} bonus speed";
        _healthCardText.text = $"Regain {GameManager.PlayerRegainLife} HP";

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

        if(state == GameManager.STATE.Over) {
            _gameOverCanvas.SetActive(true);
        }
    }

}
