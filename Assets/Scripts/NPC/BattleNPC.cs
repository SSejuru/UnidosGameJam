using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    Idle,
    Attacking,
    Healing,
    Dead
}

public class BattleNPC : LivingBeing
{
    [SerializeField]
    private LayerMask _enemyMask;
    private NPCState _currentState;

    //Timer variables
    private float _currentTime;
    private float _searchTimer = 2f;

    //EnemyTarget to attack
    private GameObject _enemy;


    // Start is called before the first frame update
    void Start()
    {
        InitializeStats();
        SetState(NPCState.Idle);
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemies();
        Attack();
    }


    #region EnemySearch

    /// <summary>
    /// Checks every couple seconds for a target
    /// </summary>
    private void CheckForEnemies()
    {
        if (_currentState != NPCState.Idle)
            return;

        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            _currentTime = _searchTimer;
            FindEnemy();
        }
    }

    //Find closest enemy
    private void FindEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, _attackDistance, _enemyMask);

        float distance = _attackDistance;
        bool targetFound = false;

        foreach (var hitCollider in hitColliders)
        {
            if (Vector2.Distance(hitCollider.transform.position, transform.position) < distance)
            {
                if (!hitCollider.GetComponent<LivingBeing>().IsDead)
                {
                    distance = Vector2.Distance(hitCollider.transform.position, transform.position);
                    _enemy = hitCollider.gameObject;
                    targetFound = true;
                }
            }
        }

        if (targetFound)
            SetState(NPCState.Attacking);
    }

    #endregion

    /// <summary>
    /// Attacks target with timer
    /// </summary>
    private void Attack()
    {
        if (_currentState != NPCState.Attacking)
            return;

        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            _currentTime = _attackTimer;

            //Attack and check for state change
            _enemy.GetComponent<LivingBeing>().ApplyDamage(_attackDamage);

            if (_enemy.GetComponent<LivingBeing>().IsDead)
            {
                SetState(NPCState.Idle);
            }
        }
    }

    private void SetState(NPCState state)
    {
        _currentState = state;

        switch (state)
        {
            case NPCState.Idle:
                //Play Idle animation
                _currentTime = 0;
                break;
            case NPCState.Attacking:
                _currentTime = _attackTimer;
                break;
            case NPCState.Healing:
                break;
            case NPCState.Dead:
                break;
        }
    }

    public override void Die()
    {
        base.Die();
        SetState(NPCState.Dead);
    }
}
