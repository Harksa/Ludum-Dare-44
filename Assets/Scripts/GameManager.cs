using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager {

    public enum STATE {
        Running,
        Paused,
        InShop,
        Over
    };
    
    #region FONCTIONS DELEGATES
    public delegate void ScoreChange(int score);
    public delegate void HealthChange(int healthChange);
    public delegate void StateChange(STATE state);
    public delegate void WaveChange(int wave);
    public delegate void StartWave();
    public delegate void EnemyRemainingChange(int remainingEnemies);
    #endregion

    #region GESTION ETAT
    public static event StateChange StateChanged;
    private static STATE _state = STATE.Running;
    public static STATE State {
        get { return _state; }
        set {
            if (value != _state) {
                _state = value;

                switch (_state) {
                    case STATE.Running:
                        Time.timeScale = 1;
                        break;
                    case STATE.Paused:
                    case STATE.Over:
                    case STATE.InShop:
                        Time.timeScale = 0;
                        break;
                }

                if (StateChanged != null) {
                    StateChanged(_state);
                }
            }
        }
    }
    #endregion
 
    #region GESTION SCORE
    public static int BaseEnemyScore = 10;
    public static int EnemyScoreGrow = 5;

    public static event ScoreChange HighScoreChanged;
    public static int HighScore {
        get {
            return PlayerPrefs.GetInt("HighScore", 0);
        }
        set {
            PlayerPrefs.SetInt("HighScore", value);

            if (HighScoreChanged != null) {
                HighScoreChanged(value);
            }
        }
    }

    public static event ScoreChange ScoreChanged;
    private static int _score;
    public static int Score {
        get { return _score; }
        set {
            if (value != _score) {
                _score = value;

                if (ScoreChanged != null) {
                    ScoreChanged(_score);
                }
                
                if (_score > HighScore) {
                    HighScore = _score;
                }
            }
        }
    }
    #endregion

    #region GESTION JOUEUR

    public const int _maxHealth = 100;

    public static event HealthChange MaxhHealthChanged;

    private static int _currentMaxHealth;
    public static int CurrentMaxHealth
    {
        get { return _currentMaxHealth;}
        set { 
            _currentMaxHealth = value;

            if(MaxhHealthChanged != null)
                MaxhHealthChanged(_currentMaxHealth);

            if(PlayerHealth > CurrentMaxHealth)
                PlayerHealth = CurrentMaxHealth;
        }
    }

    public static event HealthChange HealthChanged;
    private static int _health;
    public static int PlayerHealth {
        get { return _health; }
        set {
            if (value != _health) {
                _health = value;

                if (_health <= 0) {
                    _health = 0;
                    State = STATE.Over;
                }

                if(_health > CurrentMaxHealth) {
                    _health = CurrentMaxHealth;
                }
                                
                if (HealthChanged != null) {
                    HealthChanged(_health);
                }
            }
        }
    }
 
    private static float _startingDamages = 10;
    public static float PlayerDamages;

    public readonly static float PlayerIncreaseDamages = 4;

    private static float _startingFireRate = 0.3f;
    public static float PlayerFireRate;

    public readonly static float PlayerIncreaseFireRate = 0.03f;
    
    private static float _startingSpeed = 5;
    public static float PlayerSpeed;

    public readonly static float PlayerIncreaseSpeed = 0.2f;

    public readonly static int PlayerRegainLife = 7;
    public readonly static int HPLostForBonus = 10;

    #endregion 

    #region GESTION ENNEMIS
        public static float EnemyHealth = 50;
        public static float _startingEnemySpeed = 3.25f;
        public static float EnemySpeed;
        public const float _enemySpeedUpPerWave = 0.25f;
    #endregion

    #region GESTION VAGUES
        public static event WaveChange WaveChanged;
        private static int _currentWave = 1;
        public static int CurrentWave
        {
            get { return _currentWave;}
            set { 
                _currentWave = value;
            
                if(WaveChanged != null)
                    WaveChanged(_currentWave);
            }
        }

        private static int _startingNumberToSpawn = 5;
        public static int RemainingEnemiesToSpawn = 5;
        private static float _growingRate = 1.1f;
        public static float SpawningRate = 1;
        private static float _spawningIncrasingRate = 0.07f;

        public static event EnemyRemainingChange EnemyRemainingChanged;
        private static  int _remainingEnemies;
        public static  int RemainingEnemies
        {
            get { return _remainingEnemies;}
            set { 
                _remainingEnemies = value;

                if(_remainingEnemies <= 0) {
                    State = STATE.InShop;
                }
                
                if(EnemyRemainingChanged != null)
                    EnemyRemainingChanged(_remainingEnemies);
                
            }
        }
        
        public static event StartWave OnStartWave;
        public static void StartNextWave() {
            CurrentWave ++;

            RemainingEnemiesToSpawn = (int) ((_startingNumberToSpawn + 5 * (CurrentWave - 1)) * _growingRate);
            RemainingEnemies = RemainingEnemiesToSpawn;
            SpawningRate -= _spawningIncrasingRate;
            if(SpawningRate < 0) SpawningRate = 0;    
            EnemySpeed += _enemySpeedUpPerWave;

            State = STATE.Running;

            if(OnStartWave != null) {
                OnStartWave();
            }
        }

    #endregion

    public static void StartGame() {

        ObjectPool.ClearPool();

        CurrentMaxHealth = _maxHealth;
        PlayerHealth = _maxHealth;
        PlayerDamages = _startingDamages;
        PlayerFireRate = _startingFireRate;
        PlayerSpeed = _startingSpeed;
        Score = 0;

        CurrentWave = 1;

        RemainingEnemiesToSpawn = _startingNumberToSpawn;
        RemainingEnemies = _startingNumberToSpawn;

        EnemySpeed = _startingEnemySpeed;

        ScoreChanged = null;
        HighScoreChanged = null;
        HealthChanged = null;
        StateChanged = null;
        WaveChanged = null;
        EnemyRemainingChanged = null;
        OnStartWave = null;

        State = STATE.Running;
        
        if(OnStartWave != null) {
            OnStartWave();
        }
    }


}
