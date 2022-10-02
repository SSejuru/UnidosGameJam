using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingBeing
{
    [SerializeField] protected float _maxMana = 80f;
    [SerializeField] protected float _manaRegenRate = 0.5f;

    protected float _currentMana;
    private float _manaTimer = 0f;

    public float MaxMana { get => _maxMana; }
    public float Mana { get => _currentMana; }

    private Rigidbody2D _rigidBody;

    private bool _canMove = true;

    private PlayerInteraction _interactionComp;
    public PlayerInteraction InteractionComp { get => _interactionComp; }
    public bool CanMove { get => _canMove; set => _canMove = value; }

    private float STARTING_MANA_REGEN_RATE;

    private void Awake()
    {
        _interactionComp = GetComponent<PlayerInteraction>();
        STARTING_MANA_REGEN_RATE = _manaRegenRate;
    }

    public void SetMovingStatus(bool canMove)
    {
        _canMove = canMove;

        if (!canMove) // If player stops moving, make sure it stays in the same place until movement is active again
        {
            _rigidBody.isKinematic = true;
            _rigidBody.velocity = Vector3.zero;
        }
        else
            _rigidBody.isKinematic = false;

    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        InitializeStats();
        _manaRegenRate = 0f;
    }

    public void Initialize()
    {
        _manaRegenRate = STARTING_MANA_REGEN_RATE;
        AddMana(0, false);
        ManagerLocator.Instance._uiManager.ManaPerSecondUIUpdate(_manaRegenRate);
    }


    protected void Update()
    {
        CheckForManaRegen();
        MovePlayer();
        AnimatePlayer();
    }

    protected void CheckForManaRegen()
    {
        if (_manaTimer <= 1)
        {
            _manaTimer += Time.deltaTime;
        }
        else
        {
            _manaTimer = 0;
            _currentMana += _manaRegenRate;
            Mathf.Clamp(_currentMana, 0, _maxMana);
            ManagerLocator.Instance._uiManager.UIManaUpdate(_currentMana);
        }
    }

    public void AddMana(float manaPoints, bool displayAnimation = true)
    {
        _currentMana += manaPoints;

        Mathf.Clamp(_currentMana, 0, _maxMana);

        ManagerLocator.Instance._uiManager.UIManaUpdate(_currentMana);

        if (displayAnimation)
            ManagerLocator.Instance._uiManager.DisplayManaAnimation(manaPoints);
    }

    public void AddManaRegenRate(float rate)
    {
        _manaRegenRate += rate;
        ManagerLocator.Instance._uiManager.ManaPerSecondUIUpdate(_manaRegenRate);
    }

    private void AnimatePlayer()
    {
        _animator.SetBool("IsMoving", _rigidBody.velocity.magnitude > 0);
    }

    private void MovePlayer()
    {
        if (!_canMove) // Return if player cant move
            return;

        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            _spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        direction.Normalize();
        
        _rigidBody.velocity = _movementSpeed * direction;
    }
}
