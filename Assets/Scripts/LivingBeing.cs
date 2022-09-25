using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour
{

    [Header("Character Stats")]
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _maxMana = 80f;
    [SerializeField] protected float _shieldHealth = 0f;
    [SerializeField] protected float _movementSpeed = 3f;
    [SerializeField] protected bool _usesMana = true;
    [SerializeField] protected float _manaRegenRate = 0.5f;

    [Header("Attack Stats")]
    [SerializeField] protected float _attackTimer = 1f; // Attacks every X seconds
    [SerializeField] protected float _attackDamage = 10f;
    [SerializeField] protected float _attackDistance = 10f;

    private SpriteRenderer _spriteRenderer;

    protected float _currentHealth;
    protected float _currentMana;
    protected bool _isDead = false;

    public bool IsDead { get => _isDead; }
    public float MaxHealth { get => _maxHealth; }
    public float MaxMana { get => _maxMana; }
    public float Health { get => _currentHealth; }
    public float Mana { get => _currentMana; }

    private float _deathAnimTimer = 0.7f;
    private float _manaTimer = 0f;

    protected void InitializeStats()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentHealth = _maxHealth;
        _currentMana = _maxMana;
    }

    /// <summary>
    /// Applies damage to target
    /// </summary>
    /// <param name="damage"></param>
    public virtual void ApplyDamage(float damage)
    {
        Debug.Log(gameObject.name + " recieved " + damage + " damage.");

        //Apply damage to shield
        _shieldHealth -= damage;

        //If damage destroys shield set shield to 0 and substract health
        if(_shieldHealth < 0)
        {
            _currentHealth += _shieldHealth;
            _shieldHealth = 0;
        }

        Debug.Log(gameObject.name + " Health:  " + _currentHealth + ".");

        Mathf.Clamp(_currentHealth, 0 , _maxHealth);

        ManagerLocator.Instance._uiManager.UIHealthUpdate(_currentHealth, this);
        ManagerLocator.Instance._uiManager.UIShieldUpdate(_shieldHealth, this);


        //Check if died
        if (_currentHealth <= 0)
        {       
            Die();
        }
    } 

    protected virtual void Update()
    {
        CheckForManaRegen();
    }

    protected void CheckForManaRegen()
    {
        if (!_usesMana)
            return;

        if(_manaTimer <= 1)
        {
            _manaTimer += Time.deltaTime;
        }
        else
        {
            _manaTimer = 0;
            _currentMana += _manaRegenRate;
            Mathf.Clamp(_currentMana, 0, _maxMana);
            ManagerLocator.Instance._uiManager.UIManaUpdate(_currentMana, this);
        }

    }

    public virtual void Heal(float heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        ManagerLocator.Instance._uiManager.UIHealthUpdate(_currentHealth, this);
    }

    public void GiveShield(float shield)
    {
        _shieldHealth += shield;
        ManagerLocator.Instance._uiManager.UIShieldUpdate(_shieldHealth, this);
    }

    public virtual void AddMana(float manaPoints)
    {
        _currentMana += manaPoints;

        if (_currentMana > _maxMana)
            _currentMana = _maxMana;

        ManagerLocator.Instance._uiManager.UIManaUpdate(_currentMana, this);
    }

    public void AddManaRegenRate(float rate)
    {
        _manaRegenRate += rate;
    }

    public void IncreaseAttackSpeed(float attack)
    {
        _attackTimer -= attack;
    }

    public void SlowAttackSpeed()
    {
        _attackTimer += _attackTimer / 2;
    }

    public void SlowMovement()
    {
        _movementSpeed = _movementSpeed / 2;
    }

    public virtual void Die()
    {
        _isDead = true;
        StartCoroutine(PlayDeadAnimation());
        Debug.Log(gameObject.name + " died.");
    }

    protected IEnumerator PlayDeadAnimation()
    {
        Color currentColor = _spriteRenderer.color;

        float timeRate = 1 / _deathAnimTimer;
        float time = 0f;

        while(time <= 1)
        {
            time += Time.deltaTime * timeRate;
            currentColor.a = Mathf.Lerp(1, 0, time);
            _spriteRenderer.color = currentColor;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
        yield return null;
    }
}
