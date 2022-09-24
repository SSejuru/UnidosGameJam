using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _movementSpeed;

    private Animator _animator;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
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
