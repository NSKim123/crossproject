using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _InputJumpDirection;

    private bool _IsJump;

    private bool _IsGrounded;

    private Rigidbody _Rigidbody;

    private BoxCollider _BoxCollider;

    private float _JumpForce = 3.0f;

    public bool isJump => _IsJump;

    public Vector3 velocity
    {
        get
        {
            return _Rigidbody.velocity;
        }
        set
        {
            _Rigidbody.velocity = value;
        }
    }

    public BoxCollider boxCollider => _BoxCollider ?? (_BoxCollider = GetComponent<BoxCollider>());

    public event Action<Vector2> onJump;    

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        CalculateY();

        _IsGrounded = CheckGrounded();

        CalculateX();
    }

    private void CalculateX()
    {
        if(_IsJump)
        {
            return;
        }

        if (_IsGrounded)
        {
            velocity = Vector3.zero;
        }
    }

    private void CalculateY()
    {
        ApplyGravity();

        if (_IsJump)
        {
            Jump();            
        }                
    }

    private void ApplyGravity()
    {
        float velocityY = velocity.y;
        velocityY += Physics.gravity.y * Time.fixedDeltaTime;
        velocity = new Vector3(velocity.x, velocityY, velocity.z);
    }

    private bool CheckGrounded()
    {
        Vector3 halfSize = boxCollider.size * 0.5f;
        halfSize.y = 0.0f;

        bool isHit = Physics.BoxCast(
            transform.position + boxCollider.center,
            halfSize,
            (Vector3.up * Mathf.Sign(velocity.y)),
            out RaycastHit hitInfo,
            Quaternion.identity,
            boxCollider.size.y * 0.5f + Mathf.Abs(velocity.y * Time.fixedDeltaTime),
            1 << LayerMask.NameToLayer(Constants.LAYERNAME_GROUND),
            QueryTriggerInteraction.UseGlobal
            );
        
        if(isHit)
        {
            float distance = hitInfo.distance - boxCollider.size.y * 0.5f;
            Vector3 a = velocity;
            a.y = distance * Mathf.Sign(velocity.y);
            velocity = a;
        }

        return isHit;
    }

    private void Jump()
    {
        velocity = new Vector3(
            _InputJumpDirection.x * 3.0f, 
            _JumpForce, 
            0.0f);
        _IsJump = false;

        Vector3 lookRotation = new Vector3(_InputJumpDirection.x, 0.0f, _InputJumpDirection.y);
        transform.rotation = Quaternion.LookRotation(lookRotation);

        onJump?.Invoke(_InputJumpDirection);
    }

    public void ResetMovement()
    {
        transform.position = Vector3.up * 1.5f;
        transform.rotation = Quaternion.identity;
    }

    public void OnJumpInput(Vector2 inputValue)
    {
        if (_IsGrounded)
        {
            _InputJumpDirection = inputValue;
            _IsJump = true;
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;

    //    Vector3 Size = boxCollider.size;
    //    Size.y = 0.0f;
    //    Gizmos.DrawWireCube(transform.position, Size);


    //}
}
