using System;
using UnityEngine;

/// <summary>
/// �÷��̾��� �̵��� ���õ� ������Ʈ�Դϴ�.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// ��� �������� ���������� ��Ÿ���ϴ�.
    /// </summary>
    private Vector2 _InputJumpDirection;

    /// <summary>
    /// ���� �Է��� ���Դ����� ��Ÿ���ϴ�.
    /// </summary>
    private bool _IsJumpInput;

    /// <summary>
    /// �ٴڿ� ����ִ����� ��Ÿ���ϴ�.
    /// </summary>
    private bool _IsGrounded;

    /// <summary>
    /// �÷��̾��� Rigidbody ������Ʈ�� ��Ÿ���ϴ�.
    /// </summary>
    private Rigidbody _Rigidbody;

    /// <summary>
    /// �÷��̾��� �浹ü�� ��Ÿ���ϴ�.
    /// </summary>
    private BoxCollider _BoxCollider;

    /// <summary>
    /// �������� ��Ÿ���ϴ�.
    /// </summary>
    private float _JumpForce = 3.0f;

    /// <summary>
    /// _IsJumpInput�� ���� �б����� ������Ƽ�Դϴ�.
    /// </summary>
    public bool isJumpInput => _IsJumpInput;

    /// <summary>
    /// Rigidbody�� velocity ������Ƽ�� �������� ������Ƽ�Դϴ�.
    /// </summary>
    public Vector3 velocity
    {
        get
        {
            return _Rigidbody.velocity;
        }
        private set
        {
            _Rigidbody.velocity = value;
        }
    }

    /// <summary>
    /// �÷��̾��� �浹ü�� ���� �б� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public BoxCollider boxCollider => _BoxCollider ?? (_BoxCollider = GetComponent<BoxCollider>());

    /// <summary>
    /// ������ ������ �� ������ �븮���Դϴ�.
    /// </summary>
    public event Action<Vector2> onJump;    

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        // y�� �ӵ� ���
        CalculateY();

        // ���� ����ִ��� üũ�մϴ�.
        _IsGrounded = CheckGrounded();

        // x�� �ӵ� ���
        CalculateX();
    }

    /// <summary>
    /// x�� �ӵ��� ����մϴ�.
    /// </summary>
    private void CalculateX()
    {
        // ���� �Է��� ���� ���¶�� �Լ��� �����մϴ�.
        if(_IsJumpInput)
        {
            return;
        }

        // ���� ����ִٸ� �ӵ��� �����ͷ� �����մϴ�.
        // ���� ���������� ���� �̵� ��, x �� �ӵ��� 0���� ����� ���� �����Դϴ�.
        if (_IsGrounded)
        {
            velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// y�� �ӵ��� ����մϴ�.
    /// </summary>
    private void CalculateY()
    {
        // �߷��� �����մϴ�.
        ApplyGravity();

        // ���� �Է��� �޾Ҵٸ� ������ �����մϴ�.
        if (_IsJumpInput)
        {
            Jump();            
        }                
    }

    /// <summary>
    /// �߷��� �����մϴ�.
    /// </summary>
    private void ApplyGravity()
    {
        float velocityY = velocity.y;
        velocityY += Physics.gravity.y * Time.fixedDeltaTime;
        velocity = new Vector3(velocity.x, velocityY, velocity.z);
    }

    /// <summary>
    /// ���� ��������� üũ�մϴ�.
    /// </summary>
    /// <returns> ���� ����ִٸ� true �� ��ȯ�մϴ�.</returns>
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

    /// <summary>
    /// ������ �����մϴ�.
    /// </summary>
    private void Jump()
    {
        // ���� �ӵ� ����
        velocity = new Vector3(
            _InputJumpDirection.x * 3.0f, 
            _JumpForce, 
            0.0f);

        _IsJumpInput = false;

        // ĳ���� ȸ��
        Vector3 lookRotation = new Vector3(_InputJumpDirection.x, 0.0f, _InputJumpDirection.y);
        transform.rotation = Quaternion.LookRotation(lookRotation);

        // ���� �븮�� ����
        onJump?.Invoke(_InputJumpDirection);
    }

    /// <summary>
    /// �÷��̾��� ��ġ�� ȸ������ �ʱ�ȭ�մϴ�.
    /// ������ �ٽ� ������ �� ȣ��˴ϴ�.
    /// </summary>
    public void ResetMovement()
    {
        transform.position = Vector3.up * 1.5f;
        transform.rotation = Quaternion.identity;
    }
    
    /// <summary>
    /// ����Ű �Է��� �޾��� �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    /// <param name="inputValue"></param>
    public void OnJumpInput(Vector2 inputValue)
    {
        if (_IsGrounded)
        {
            _InputJumpDirection = inputValue;
            _IsJumpInput = true;
        }
    }
}
