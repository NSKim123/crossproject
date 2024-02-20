using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerCharacter ��ü�� �Է°��� �����ϴ� ������ �ϴ� ���� ������Ʈ�� ������Ʈ�Դϴ�.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// ��Ʈ���ϰ� �ִ� ĳ���͸� ��Ÿ���ϴ�.
    /// </summary>
    private PlayerCharacter _ControlledCharacter;

    /// <summary>
    /// ��Ʈ���ϰ� �ִ� ĳ���Ϳ� ���� �б����� ������Ƽ�Դϴ�.
    /// </summary>
    public PlayerCharacter controlledCharacter => _ControlledCharacter;

    /// <summary>
    /// UpArrow Ű�� ������ �� ȣ��Ǵ� �޼����Դϴ�.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpForward(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(0.0f, 1.0f));
    }

    /// <summary>
    /// DownArrow Ű�� ������ �� ȣ��Ǵ� �޼����Դϴ�.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpBackward(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(0.0f, -1.0f));
    }

    /// <summary>
    /// LeftArrow Ű�� ������ �� ȣ��Ǵ� �޼����Դϴ�.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpLeft(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(-1.0f, 0.0f));
    }

    /// <summary>
    /// RightArrow Ű�� ������ �� ȣ��Ǵ� �޼����Դϴ�.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpRight(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(1.0f, 0.0f));
    }

    /// <summary>
    /// �÷��̾� ĳ���͸� ��Ʈ�� �����ϴ� �޼����Դϴ�.
    /// </summary>
    /// <param name="newCharacter"> ������ ĳ���͸� �Ѱ��ݴϴ�.</param>
    public void StartControlCharacter(PlayerCharacter newCharacter)
    {
        if (_ControlledCharacter == newCharacter) return;

        _ControlledCharacter = newCharacter;
        newCharacter.OnStartControlled(this);
    }
}
