using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemyToSpawn;
    private int _enemyId;

    [SerializeField] private List<GameObject> _spawnPoints;

    void Start()
    {
        ObjectPool.InitPool(_enemyToSpawn, 50);
        _enemyId = _enemyToSpawn.GetInstanceID();
        
        GameManager.OnStartWave += delegate () {
            StartCoroutine(Wave());
        };

        GameManager.Start();
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
        ObjectPool.GetInstance(_enemyId, _spawnPoints[randomPos].transform.position);
        GameManager.RemainingEnemiesToSpawn --;
    }
}
