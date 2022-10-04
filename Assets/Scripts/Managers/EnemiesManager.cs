using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();
    private List<Enemy> _enemies = new List<Enemy>();

    public void AddEnemySpawner(EnemySpawner spawner)
    {
        _enemySpawners.Add(spawner);
    }

    public void InitializeAllSpawners()
    {
        foreach (EnemySpawner spawner in _enemySpawners)
        {
            spawner.Initialize();
        }
    }

    public void StopAllSpawners()
    {
        foreach (EnemySpawner spawner in _enemySpawners)
        {
            spawner.StopSpawner();
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
   
    public void SlowEnemies()
    {
        for(int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].SlowMovement();
        }
    }

    public void ReduceEnemiesAttackSpeed()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].SlowAttackSpeed();
        }
    }

    public void DESTROYEVERYTHING()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].ApplyDamage(100000);
        }
    }
}
