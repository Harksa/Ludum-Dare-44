using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemyToSpawn = null;
    private int _enemyId;

    [SerializeField] private List<GameObject> _spawnPoints = null;

    void Start()
    {
        ObjectPool.InitPool(_enemyToSpawn, 30);
        _enemyId = _enemyToSpawn.GetInstanceID();
        
        GameManager.OnStartWave += delegate () {
            StartCoroutine(Wave());
        };

        StartCoroutine(Wave());
    }

    public 

    IEnumerator Wave() {
        while(GameManager.RemainingEnemiesToSpawn > 0) {
            SpawnEnemy();
            yield return new WaitForSeconds(GameManager.SpawningRate);
        }
    }

    private void SpawnEnemy() {
        int randomPos = Random.Range(0, _spawnPoints.Count);
        GameObject go = ObjectPool.GetInstance(_enemyId, _spawnPoints[randomPos].transform.position);
        EnemyHealthManager health = go.GetComponent<EnemyHealthManager>();
        health.currentHealth = GameManager.EnemyHealth;
        GameManager.RemainingEnemiesToSpawn--;
    }
}
