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
    [SerializeField] private Animator _animator;
    [SerializeField] private float _searchRadius = 20f;

    private GameObject _target;
    private float _searchTimer = 2f;

    private EnemyState _currentState;
    private float _currentTime;

    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        InitializeStats();
        _rigidBody = GetComponent<Rigidbody2D>();
        SetState(EnemyState.SearchingTarget);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTargetSearch();
        Move();
        Attack();
    }

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

    //Find closest target
    private void FindTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, _searchRadius, _searchMask);

        float distance = _searchRadius;
        bool targetFound = false;

        foreach (var hitCollider in hitColliders)
        {
            if (Vector2.Distance(hitCollider.transform.position, transform.position) < distance)
            {
                if (!hitCollider.GetComponent<LivingBeing>().IsDead)
                {
                    distance = Vector2.Distance(hitCollider.transform.position, transform.position);
                    _target = hitCollider.gameObject;
                    targetFound = true;
                }
            }
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
            _target.GetComponent<LivingBeing>().ApplyDamage(_attackDamage);

            if (_target.GetComponent<LivingBeing>().IsDead)
            {
                SetState(EnemyState.SearchingTarget);
            }
        }
    }  

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, _searchRadius);
    }

    private void SetState(EnemyState state)
    {
        _currentState = state;
        _rigidBody.isKinematic = false;

        //Execute animation for each
        switch (state)
        {
            case EnemyState.SearchingTarget:
                _rigidBody.isKinematic = true;
                _currentTime = 0f;
                break;
            case EnemyState.Moving:
                break;
            case EnemyState.Attacking:
                _currentTime = _attackTimer;
                break;
            case EnemyState.Dead:
                _rigidBody.isKinematic = true;
                break;
        }
    }

    public override void Die()
    {
        base.Die();

        SetState(EnemyState.Dead);
    }


}
