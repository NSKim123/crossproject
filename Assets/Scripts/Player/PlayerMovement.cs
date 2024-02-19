using System;
using UnityEngine;

/// <summary>
/// 플레이어의 이동과 관련된 컴포넌트입니다.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// 어느 방향으로 점프할지를 나타냅니다.
    /// </summary>
    private Vector2 _InputJumpDirection;

    /// <summary>
    /// 점프 입력이 들어왔는지를 나타냅니다.
    /// </summary>
    private bool _IsJumpInput;

    /// <summary>
    /// 바닥에 닿아있는지를 나타냅니다.
    /// </summary>
    private bool _IsGrounded;

    /// <summary>
    /// 플레이어의 Rigidbody 컴포넌트를 나타냅니다.
    /// </summary>
    private Rigidbody _Rigidbody;

    /// <summary>
    /// 플레이어의 충돌체를 나타냅니다.
    /// </summary>
    private BoxCollider _BoxCollider;

    /// <summary>
    /// 점프력을 나타냅니다.
    /// </summary>
    private float _JumpForce = 3.0f;

    /// <summary>
    /// _IsJumpInput에 대한 읽기전용 프로퍼티입니다.
    /// </summary>
    public bool isJumpInput => _IsJumpInput;

    /// <summary>
    /// Rigidbody의 velocity 프로퍼티를 가져오는 프로퍼티입니다.
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
    /// 플레이어의 충돌체에 대한 읽기 전용 프로퍼티입니다.
    /// </summary>
    public BoxCollider boxCollider => _BoxCollider ?? (_BoxCollider = GetComponent<BoxCollider>());

    /// <summary>
    /// 점프를 시작할 때 실행할 대리자입니다.
    /// </summary>
    public event Action<Vector2> onJump;    

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        // y축 속도 계산
        CalculateY();

        // 땅에 닿아있는지 체크합니다.
        _IsGrounded = CheckGrounded();

        // x축 속도 계산
        CalculateX();
    }

    /// <summary>
    /// x축 속도를 계산합니다.
    /// </summary>
    private void CalculateX()
    {
        // 점프 입력을 받은 상태라면 함수를 종료합니다.
        if(_IsJumpInput)
        {
            return;
        }

        // 땅에 닿아있다면 속도를 영벡터로 설정합니다.
        // 왼쪽 오른쪽으로 점프 이동 후, x 축 속도를 0으로 만들기 위한 구문입니다.
        if (_IsGrounded)
        {
            velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// y축 속도를 계산합니다.
    /// </summary>
    private void CalculateY()
    {
        // 중력을 적용합니다.
        ApplyGravity();

        // 점프 입력을 받았다면 점프를 실행합니다.
        if (_IsJumpInput)
        {
            Jump();            
        }                
    }

    /// <summary>
    /// 중력을 적용합니다.
    /// </summary>
    private void ApplyGravity()
    {
        float velocityY = velocity.y;
        velocityY += Physics.gravity.y * Time.fixedDeltaTime;
        velocity = new Vector3(velocity.x, velocityY, velocity.z);
    }

    /// <summary>
    /// 땅에 닿아있음을 체크합니다.
    /// </summary>
    /// <returns> 땅에 닿아있다면 true 를 반환합니다.</returns>
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
    /// 점프를 적용합니다.
    /// </summary>
    private void Jump()
    {
        // 점프 속도 설정
        velocity = new Vector3(
            _InputJumpDirection.x * 3.0f, 
            _JumpForce, 
            0.0f);

        _IsJumpInput = false;

        // 캐릭터 회전
        Vector3 lookRotation = new Vector3(_InputJumpDirection.x, 0.0f, _InputJumpDirection.y);
        transform.rotation = Quaternion.LookRotation(lookRotation);

        // 점프 대리자 실행
        onJump?.Invoke(_InputJumpDirection);
    }

    /// <summary>
    /// 플레이어의 위치와 회전값을 초기화합니다.
    /// 게임을 다시 시작할 때 호출됩니다.
    /// </summary>
    public void ResetMovement()
    {
        transform.position = Vector3.up * 1.5f;
        transform.rotation = Quaternion.identity;
    }
    
    /// <summary>
    /// 점프키 입력을 받았을 때 호출되는 메서드입니다.
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
