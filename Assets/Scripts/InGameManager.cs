using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{

    void Start()
    {
        GameManager.CurrentMaxHealth = GameManager.maxHealth;
        GameManager.Health = GameManager.maxHealth;
        GameManager.Score = 0;
    }

}
