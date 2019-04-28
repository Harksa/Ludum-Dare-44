using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    void Start()
    {
        GameManager.CurrentMaxHealth = GameManager._maxHealth;
        GameManager.PlayerHealth = GameManager._maxHealth;
        GameManager.Score = 0;
    }

}
