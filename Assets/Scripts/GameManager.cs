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
    public delegate void DamageChange(float damages);
    public delegate void WaveChange(float wave);
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
    
    #region GESTION VIE
    public const int maxHealth = 100;

    public static event HealthChange MaxhHealthChanged;

    private static int _currentMaxHealth;
    public static int CurrentMaxHealth
    {
        get { return _currentMaxHealth;}
        set { 
            _currentMaxHealth = value;

            if(MaxhHealthChanged != null)
                MaxhHealthChanged(_currentMaxHealth);

            if(Health > CurrentMaxHealth)
                Health = CurrentMaxHealth;
        }
    }

    public static event HealthChange HealthChanged;
    private static int _health;
    public static int Health {
        get { return _health; }
        set {
            if (value != _health) {
                _health = value;

                if (HealthChanged != null) {
                    HealthChanged(value);
                }

                if (_health <= 0) {
                    State = STATE.Over;
                    return;
                }

                if(_health > CurrentMaxHealth) {
                    _health = CurrentMaxHealth;
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

    #region GESTION DOMMAGES
    private static float StartingDamages = 10;
    
    public static event DamageChange DamageChanged;
    private static float _damages = 10;
    
    public static float Damages
    {
        get { return _damages;}
        set { 
            _damages = value;

            if(DamageChanged != null) {
                DamageChanged(value);
            }
        }
    }   
    #endregion 

    #region GESTION ENNEMIS
        public static float EnemyHealth = 50;
        public static float StartingEnemySpeed = 3;
        public static float EnemySpeed = 3;
        public const float EnemySpeedUpPerWave = 0.3f;
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

        private static int StartingNumberToSpawn = 5;
        public static int RemainingEnemiesToSpawn = 5;
        private static float _growingRate = 1.12f;
        public static float SpawningRate = 1;
        private static float _spawningIncrasingRate = 0.04f;

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

            RemainingEnemiesToSpawn = (int) ((StartingNumberToSpawn + 5 * (CurrentWave - 1)) * _growingRate);
            RemainingEnemies = RemainingEnemiesToSpawn;
            SpawningRate -= _spawningIncrasingRate;
            
            EnemySpeed += EnemySpeedUpPerWave;

            State = STATE.Running;

            if(OnStartWave != null) {
                OnStartWave();
            }
        }

    #endregion

    public static void Start() {
        Health = maxHealth;
        Damages = StartingDamages;
        Score = 0;

        RemainingEnemiesToSpawn = StartingNumberToSpawn;
        RemainingEnemies = StartingNumberToSpawn;

        EnemySpeed = StartingEnemySpeed;

        State = STATE.Running;

        if(OnStartWave != null) {
            OnStartWave();
        }
    }

    public static void Exit() {
        ScoreChanged = null;
        HighScoreChanged = null;
        HealthChanged = null;
        StateChanged = null;
    }

    public static void Restart() {
        Start();
        
        //TODO RESET ALL OBJECT INSTEAD OF RELOADING
        SceneManager.LoadScene(0);

    }
}
