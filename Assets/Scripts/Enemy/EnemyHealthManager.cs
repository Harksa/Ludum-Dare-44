using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    [SerializeField] private float maxHealth = 50;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage() {
        currentHealth -= GameManager.Damages;
        if(currentHealth <= 0) {
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
