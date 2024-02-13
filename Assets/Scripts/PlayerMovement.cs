using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int _InputMoveDirection;

    private int _InputJumpDirection;

    private bool _IsJump;

    private Rigidbody _Rigidbody;

    private BoxCollider _BoxCollider;

    private float _Speed = 1.0f;

    public bool isJump => _IsJump;

    public BoxCollider boxCollider => _BoxCollider ?? (_BoxCollider = GetComponent<BoxCollider>());

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Jump();

        Move();
    }

    private void Move()
    {
        if(isJump)
        {
            _InputMoveDirection = 0;
            return;
        }

        transform.position += Vector3.right * _InputMoveDirection * _Speed * Time.fixedDeltaTime;
        _InputMoveDirection = 0;
    }

    private void Jump()
    {
        if (_InputJumpDirection == 0 || isJump)
        {   
            if(_Rigidbody.velocity.y == 0.0f)
            {
                _IsJump = false;
            }
            return;
        }

        _Rigidbody.AddForce(new Vector3(0.0f, 3.0f, _InputJumpDirection * 1.67f), ForceMode.Impulse);
        _InputJumpDirection = 0;
        _IsJump = true;
    }

    public void OnMoveInput(int inputValue)
    {
        _InputMoveDirection = inputValue;
    }

    public void OnJumpInput(int jumpDirection)
    {
        _InputJumpDirection = jumpDirection;        
    }
}
