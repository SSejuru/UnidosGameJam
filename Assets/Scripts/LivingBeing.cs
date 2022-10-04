using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour
{

    [Header("Character Stats")]
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _shieldHealth = 0f;
    [SerializeField] protected float _movementSpeed = 3f;   

    [Header("Attack Stats")]
    [SerializeField] protected float _attackTimer = 1f; // Attacks every X seconds
    [SerializeField] protected float _attackDamage = 10f;
    [SerializeField] protected float _attackDistance = 10f;

    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected float _maxShield;
    protected float _currentHealth;
    protected bool _isDead = false;

    public bool IsDead { get => _isDead; }
    public float MaxHealth { get => _maxHealth; }   
    public float Health { get => _currentHealth; } 

    private float _deathAnimTimer = 0.7f; 
    private float _colorHurtTimer = 0.5f;

    private IEnumerator damageAnimation;

    protected void InitializeStats()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
        _maxShield = MaxHealth;
    }

    /// <summary>
    /// Applies damage to target
    /// </summary>
    /// <param name="damage"></param>
    public virtual void ApplyDamage(float damage)
    {
        damageAnimation = PlayHurtAnimation();
        StartCoroutine(damageAnimation);

        //Apply damage to shield
        _shieldHealth -= damage;

        //If damage destroys shield set shield to 0 and substract health
        if(_shieldHealth < 0)
        {
            _currentHealth += _shieldHealth;
            _shieldHealth = 0;
        }    

        Mathf.Clamp(_currentHealth, 0 , _maxHealth);    

        //Check if died
        if (_currentHealth <= 0)
        {       
            Die();
        }
    } 


    public virtual void Heal(float heal)
    {
        _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;     
    }

    public virtual void GiveShield(float shield)
    {
        _shieldHealth += shield;      
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
        if(damageAnimation != null)
            StopCoroutine(damageAnimation);

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

    protected IEnumerator PlayHurtAnimation()
    {
        _spriteRenderer.color = Color.red;
        Color currentColor =_spriteRenderer.color;

        float timeRate = 1 / _colorHurtTimer;
        float time = 0f;

        while (time <= 1)
        {
            time += Time.deltaTime * timeRate;
            currentColor.g = Mathf.Lerp(0, 1, time);
            currentColor.b = Mathf.Lerp(0, 1, time);
            _spriteRenderer.color = currentColor;
            yield return new WaitForEndOfFrame();
        }
        
        yield return null;
    }
}
