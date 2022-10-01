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

    private Animator _animator;
    private Rigidbody2D _rigidBody;

    private bool _canMove = true;

    private PlayerInteraction _interactionComp;
    public PlayerInteraction InteractionComp { get => _interactionComp; }


    private void Awake()
    {
        _interactionComp = GetComponent<PlayerInteraction>();
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
        //_animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        InitializeStats();
        AddMana(20, false);
        ManagerLocator.Instance._uiManager.ManaPerSecondUIUpdate(_manaRegenRate);
    }


    protected void Update()
    {
        CheckForManaRegen();
        MovePlayer();
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


    private void MovePlayer()
    {
        if (!_canMove) // Return if player cant move
            return;

        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            // _animator.SetInteger("Direction", 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            // _animator.SetInteger("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
            //_animator.SetInteger("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
            // _animator.SetInteger("Direction", 0);
        }

        direction.Normalize();
        // _animator.SetBool("IsMoving", direction.magnitude > 0);

        _rigidBody.velocity = _movementSpeed * direction;
    }
}
