using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float currentHealth;
    public void ApplyDamage() {
        currentHealth -= GameManager.PlayerDamages;
        GameManager.Score += 1;
        if(currentHealth <= 0) {
            GameManager.Score += GameManager.BaseEnemyScore + GameManager.EnemyScoreGrow * (GameManager.CurrentWave - 1);
            GameManager.RemainingEnemies--;
            ObjectPool.Release(gameObject);
        }
     }

    private void OnCollisionEnter(Collision other) {
        if(!other.gameObject.CompareTag("Bullet")) {
            return;
        }

        ApplyDamage();
        ObjectPool.Release(other.gameObject);
        
    }
}
