using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터에 대한 컴포넌트입니다.
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    /// <summary>
    /// PlayerMovement 컴포넌트입니다.
    /// </summary>
    private PlayerMovement _MovementComponent;

    /// <summary>
    /// 자신을 조종하는 PlayerController 객체입니다.
    /// </summary>
    private PlayerController _PlayerController;

    /// <summary>
    /// PlayerCharacterAnimController 컴포넌트입니다.
    /// </summary>
    private PlayerCharacterAnimController _AnimController;

    /// <summary>
    /// 연속적인 백 점프 횟수에 대한 정보입니다.
    /// </summary>
    private int _BackwardJumpCombo;

    /// <summary>
    /// PlayerMovement 컴포넌트에 대한 읽기 전용 프로퍼티입니다.
    /// </summary>
    public PlayerMovement movementComponent => (_MovementComponent ?? GetComponent<PlayerMovement>());

    /// <summary>
    /// PlayerCharacterAnimController 컴포넌트에 대한 읽기 전용 프로퍼티입니다.
    /// </summary>
    public PlayerCharacterAnimController animController 
        => _AnimController ?? (_AnimController = GetComponentInChildren<PlayerCharacterAnimController>());

    /// <summary>
    /// _PlayerController 객체에 대한 읽기 전용 프로퍼티입니다.
    /// </summary>
    public PlayerController controller => _PlayerController;

    /// <summary>
    /// _BackwardJumpCombo 에 대한 프로퍼티입니다.
    /// </summary>
    public int backwardJumpCombo 
    {
        get
        {
            return _BackwardJumpCombo;
        }
        private set
        {
            _BackwardJumpCombo = value;

            // 백 점프를 연속 3번 사용할 경우 게임오버 대리자를 호출합니다.
            if (_BackwardJumpCombo >= 3)
                OnGameOver();
        }
    }

    /// <summary>
    /// 게임 오버 조건이 만족할 때 호출되는 대리자 입니다.
    /// </summary>
    public event Action onGameOver;

    private void Awake()
    {
        // 점프 이벤트를 바인드합니다.
        BindingJumpEvent();
    }

    /// <summary>
    /// 점프 이벤트를 바인드합니다.
    /// </summary>
    private void BindingJumpEvent()
    {        
        movementComponent.onJump +=
            (Vector2 jumpDirection) =>
            {
                animController.SetTrigger(Constants.ANIMPARAM_ISJUMP);
            };

        movementComponent.onJump += OnPlayerJump;
    }

    /// <summary>
    /// 플레이어 캐릭터를 초기화할 때 호출하는 메서드입니다.
    /// </summary>
    public void ResetPlayerCharacter()
    {
        movementComponent.ResetMovement();

        backwardJumpCombo = 0;
    }

    public void OnGameOver()
    {
        onGameOver?.Invoke();
    }

    /// <summary>
    /// 플레이어가 점프할 때 호출되는 메서드입니다.
    /// backwardJumpCombo 값을 조정합니다.
    /// 옆으로 점프하는 것은 점프 콤보를 초기화하지 않습니다.
    /// </summary>
    /// <param name="jumpDirection"> 점프 방향을 전달합니다.</param>
    public void OnPlayerJump(Vector2 jumpDirection)
    {
        // 백 점프일때
        if (jumpDirection.y < 0.0f)            
            ++backwardJumpCombo;
        // 앞 점프일때
        else if(jumpDirection.y > 0.0f)
            backwardJumpCombo = 0;        
    }

    /// <summary>
    /// 점프 키를 입력받았을 때 호출되는 메서드입니다.
    /// PlayerMovement 컴포넌트에 입력값을 전달합니다.
    /// </summary>
    /// <param name="inputValue"> 컨트롤러에게서 전달받은 입력값입니다.</param>
    public void OnJumpInput(Vector2 inputValue)
    {
        movementComponent.OnJumpInput(inputValue);
    }

    /// <summary>
    /// 자신을 조종하는 PlayerController를 갱신합니다.
    /// </summary>
    /// <param name="newController"> 조종하는 PlayerController 객체를 전달합니다. </param>
    public void OnStartControlled(PlayerController newController)
    {
        if (controller == newController) return;

        _PlayerController = newController;
    }
}
