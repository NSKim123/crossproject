using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ĳ���Ϳ� ���� ������Ʈ�Դϴ�.
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    /// <summary>
    /// PlayerMovement ������Ʈ�Դϴ�.
    /// </summary>
    private PlayerMovement _MovementComponent;

    /// <summary>
    /// �ڽ��� �����ϴ� PlayerController ��ü�Դϴ�.
    /// </summary>
    private PlayerController _PlayerController;

    /// <summary>
    /// PlayerCharacterAnimController ������Ʈ�Դϴ�.
    /// </summary>
    private PlayerCharacterAnimController _AnimController;

    /// <summary>
    /// �������� �� ���� Ƚ���� ���� �����Դϴ�.
    /// </summary>
    private int _BackwardJumpCombo;

    /// <summary>
    /// PlayerMovement ������Ʈ�� ���� �б� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public PlayerMovement movementComponent => (_MovementComponent ?? GetComponent<PlayerMovement>());

    /// <summary>
    /// PlayerCharacterAnimController ������Ʈ�� ���� �б� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public PlayerCharacterAnimController animController 
        => _AnimController ?? (_AnimController = GetComponentInChildren<PlayerCharacterAnimController>());

    /// <summary>
    /// _PlayerController ��ü�� ���� �б� ���� ������Ƽ�Դϴ�.
    /// </summary>
    public PlayerController controller => _PlayerController;

    /// <summary>
    /// _BackwardJumpCombo �� ���� ������Ƽ�Դϴ�.
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

            // �� ������ ���� 3�� ����� ��� ���ӿ��� �븮�ڸ� ȣ���մϴ�.
            if (_BackwardJumpCombo >= 3)
                OnGameOver();
        }
    }

    /// <summary>
    /// ���� ���� ������ ������ �� ȣ��Ǵ� �븮�� �Դϴ�.
    /// </summary>
    public event Action onGameOver;

    private void Awake()
    {
        // ���� �̺�Ʈ�� ���ε��մϴ�.
        BindingJumpEvent();
    }

    /// <summary>
    /// ���� �̺�Ʈ�� ���ε��մϴ�.
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
    /// �÷��̾� ĳ���͸� �ʱ�ȭ�� �� ȣ���ϴ� �޼����Դϴ�.
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
    /// �÷��̾ ������ �� ȣ��Ǵ� �޼����Դϴ�.
    /// backwardJumpCombo ���� �����մϴ�.
    /// ������ �����ϴ� ���� ���� �޺��� �ʱ�ȭ���� �ʽ��ϴ�.
    /// </summary>
    /// <param name="jumpDirection"> ���� ������ �����մϴ�.</param>
    public void OnPlayerJump(Vector2 jumpDirection)
    {
        // �� �����϶�
        if (jumpDirection.y < 0.0f)            
            ++backwardJumpCombo;
        // �� �����϶�
        else if(jumpDirection.y > 0.0f)
            backwardJumpCombo = 0;        
    }

    /// <summary>
    /// ���� Ű�� �Է¹޾��� �� ȣ��Ǵ� �޼����Դϴ�.
    /// PlayerMovement ������Ʈ�� �Է°��� �����մϴ�.
    /// </summary>
    /// <param name="inputValue"> ��Ʈ�ѷ����Լ� ���޹��� �Է°��Դϴ�.</param>
    public void OnJumpInput(Vector2 inputValue)
    {
        movementComponent.OnJumpInput(inputValue);
    }

    /// <summary>
    /// �ڽ��� �����ϴ� PlayerController�� �����մϴ�.
    /// </summary>
    /// <param name="newController"> �����ϴ� PlayerController ��ü�� �����մϴ�. </param>
    public void OnStartControlled(PlayerController newController)
    {
        if (controller == newController) return;

        _PlayerController = newController;
    }
}
