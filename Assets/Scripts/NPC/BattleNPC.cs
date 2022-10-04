using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NPCState
{
    Idle,
    Attacking,
    Healing,
    Dead
}

public class BattleNPC : LivingBeing
{
    [Space(10)]
    [Header("Health Bar")]
    [SerializeField] private GameObject _healthBarContainer;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Image _shieldBarImage;


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
        UpdateHealthBar();
        UpdateShieldBar();
        ManagerLocator.Instance._npcManager.AddNpc(this);
        SetState(NPCState.Idle);
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        CheckForEnemies();
        Attack();
    }

    public void ToggleHealthBar(bool value)
    {
        _healthBarContainer.SetActive(value);
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        UpdateHealthBar();
        UpdateShieldBar();
    }

    public override void Heal(float heal)
    {
        base.Heal(heal);
        UpdateHealthBar();
    }

    public override void GiveShield(float shield)
    {
        base.GiveShield(shield);
        UpdateShieldBar();
    }

    private void UpdateHealthBar()
    {
        _healthBarImage.fillAmount = _currentHealth / _maxHealth;
    }

    private void UpdateShieldBar()
    {
        _shieldBarImage.fillAmount = _shieldHealth / _maxShield;
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

        if (_enemy.transform.position.x < transform.position.x)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;

        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            _currentTime = _attackTimer;

            //Attack and check for state change
            _animator.SetBool("isAttacking", true);


        }
    }

    private void InflictDamage()
    {
        _enemy.GetComponent<LivingBeing>().ApplyDamage(_attackDamage);

        if (_enemy.GetComponent<LivingBeing>().IsDead)
        {
            SetState(NPCState.Idle);
        }
    }


    public void EndAtackAnim()
    {
        _animator.SetBool("isAttacking", false);
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
        SetState(NPCState.Dead);
        ManagerLocator.Instance._npcManager.RemoveNPCFromList(this);
        base.Die();
    }
}
