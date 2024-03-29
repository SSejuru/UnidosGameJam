using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    SearchingTarget,
    Moving,
    Attacking,
    Dead
}

public class Enemy : LivingBeing
{
    [Header("Enemy Data")]
    [SerializeField] private LayerMask _searchMask;
    [SerializeField] private float _searchRadius = 20f;

    private GameObject _target;
    private float _searchTimer = 2f;

    private EnemyState _currentState;
    private float _currentTime;

    private Rigidbody2D _rigidBody;

    /// <summary>
    /// Checks every couple seconds for a target
    /// </summary>
    private void CheckForTargetSearch()
    {
        if (_currentState != EnemyState.SearchingTarget)
            return;    

        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            _currentTime = _searchTimer;
            FindTarget();
        }
    }

    /// <summary>
    /// Cast a overlap circle that will pick a random target between all results
    /// </summary>
    private void FindTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position,
            _searchRadius, _searchMask);

        float distance = _searchRadius;
        bool targetFound = false;

        if(hitColliders.Length > 0)
        {
            _target = hitColliders[Random.Range(0, hitColliders.Length)].gameObject;
            targetFound = true;
        }      

        if (targetFound)
            SetState(EnemyState.Moving);
    }

    /// <summary>
    /// Moves enemy towards target until reaching attack distance
    /// </summary>
    private void Move()
    {
        if (_currentState != EnemyState.Moving)
            return;
        
        if (_target == null)
        {
            SetState(EnemyState.SearchingTarget);
            return;
        }

        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.Normalize();

        _rigidBody.velocity = directionToTarget * _movementSpeed;

        if(Vector3.Distance(transform.position, _target.transform.position) <= _attackDistance)
            SetState(EnemyState.Attacking);
    }

    /// <summary>
    /// Attacks target with timer
    /// </summary>
    private void Attack()
    {
        if (_currentState != EnemyState.Attacking)
            return;

        if (_target == null)
        {
            SetState(EnemyState.SearchingTarget);
            return;
        }

        //Stop Moving
        _rigidBody.velocity = Vector2.zero;

        //Check if player is out of attack distance
        if (Vector2.Distance(transform.position, _target.transform.position) > _attackDistance + _attackDistance / 2)
        {
            SetState(EnemyState.Moving);
            return;
        }

        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            _currentTime = _attackTimer;

            //Attack and check for state change
            _animator.SetBool("isAttacking", true);
        }
    }

    void Start()
    {
        InitializeStats();
        ManagerLocator.Instance._enemiesManager.AddEnemy(this);
        _rigidBody = GetComponent<Rigidbody2D>();
        SetState(EnemyState.SearchingTarget);
    }

    protected void Update()
    {
        CheckForTargetSearch();
        Move();
        Attack();
    }

    public void InflictDamage()
    {
        if (_target == null)
        {
            SetState(EnemyState.SearchingTarget);
            return;
        }
        _target.GetComponent<LivingBeing>().ApplyDamage(_attackDamage);

        if (_target.GetComponent<LivingBeing>().IsDead)
        {
            SetState(EnemyState.SearchingTarget);
        }
    }

    public void EndAtackAnim()
    {
        _animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        //UnityEditor.Handles.color = Color.red;
        //UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _searchRadius);
    }

    private void SetState(EnemyState state)
    {
        _currentState = state;
        _rigidBody.isKinematic = false;
        _rigidBody.velocity = Vector2.zero;

        bool isMoving = false;

        //Execute animation for each
        switch (state)
        {
            case EnemyState.SearchingTarget:
                _rigidBody.isKinematic = true;
                _currentTime = 0f;
                break;
            case EnemyState.Moving:
                isMoving = true;
                break;
            case EnemyState.Attacking:
                _currentTime = _attackTimer;
                break;
            case EnemyState.Dead:
                _rigidBody.isKinematic = true;
                break;
        }

        _animator.SetBool("isMoving", isMoving);
    }

    public override void Die()
    {
        SetState(EnemyState.Dead);
        _animator.SetBool("isAttacking", false);
        _animator.SetBool("isMoving", false);
        ManagerLocator.Instance._enemiesManager.RemoveEnemyFromList(this);
        base.Die();
    }


}
