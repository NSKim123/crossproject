using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerCharacter 객체에 입력값을 전달하는 역할을 하는 게임 오브젝트의 컴포넌트입니다.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 컨트롤하고 있는 캐릭터를 나타냅니다.
    /// </summary>
    private PlayerCharacter _ControlledCharacter;

    /// <summary>
    /// 컨트롤하고 있는 캐릭터에 대한 읽기전용 프로퍼티입니다.
    /// </summary>
    public PlayerCharacter controlledCharacter => _ControlledCharacter;

    /// <summary>
    /// UpArrow 키를 눌렀을 때 호출되는 메서드입니다.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpForward(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(0.0f, 1.0f));
    }

    /// <summary>
    /// DownArrow 키를 눌렀을 때 호출되는 메서드입니다.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpBackward(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(0.0f, -1.0f));
    }

    /// <summary>
    /// LeftArrow 키를 눌렀을 때 호출되는 메서드입니다.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpLeft(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(-1.0f, 0.0f));
    }

    /// <summary>
    /// RightArrow 키를 눌렀을 때 호출되는 메서드입니다.    
    /// </summary>
    /// <param name="input"></param>
    private void OnJumpRight(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(1.0f, 0.0f));
    }

    /// <summary>
    /// 플레이어 캐릭터를 컨트롤 시작하는 메서드입니다.
    /// </summary>
    /// <param name="newCharacter"> 조종할 캐릭터를 넘겨줍니다.</param>
    public void StartControlCharacter(PlayerCharacter newCharacter)
    {
        if (_ControlledCharacter == newCharacter) return;

        _ControlledCharacter = newCharacter;
        newCharacter.OnStartControlled(this);
    }
}
