using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnRate = 12.5f;
    [SerializeField] private float _bigSpawnRate = 50f;


    private void Start()
    {
        ManagerLocator.Instance._enemiesManager.AddEnemySpawner(this);
    }

    public void Initialize()
    {
        //Instantiate first enemy
        GameObject enemy = Instantiate(_enemyPrefab,
                                new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(-5f, 5f), 0),
                                Quaternion.identity);

        StartCoroutine(SpawnEnemy(_spawnRate, 1));
        StartCoroutine(SpawnEnemy(_bigSpawnRate, 2));
    }

    public void StopSpawner()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnEnemy(float timeRate, int enemyAmmount)
    {
        yield return new WaitForSeconds(timeRate);
        GameObject enemy;

        for(int i = 0; i < enemyAmmount; i++)
        {
            enemy = Instantiate(_enemyPrefab, 
                                new Vector3( transform.position.x + Random.Range(-5f,5f), transform.position.y + Random.Range(-5f, 5f), 0),
                                Quaternion.identity);
        }

        StartCoroutine(SpawnEnemy(timeRate, enemyAmmount));
    }

}
