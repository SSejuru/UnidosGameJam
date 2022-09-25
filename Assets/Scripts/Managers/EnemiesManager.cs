using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _enemySpawners = new List<Transform>();
    private List<Enemy> _enemies = new List<Enemy>();

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
