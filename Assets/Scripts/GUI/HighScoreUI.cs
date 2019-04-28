using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{

    private TextMeshProUGUI _highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        _highScoreText = GetComponent<TextMeshProUGUI>();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if(highScore == 0) {
            _highScoreText.enabled = false;
        } else {
            _highScoreText.text = $"HighScore : {highScore}";
        }
    }
}
