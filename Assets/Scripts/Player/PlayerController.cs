using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerCharacter _ControlledCharacter;

    public PlayerCharacter controlledCharacter => _ControlledCharacter;

    private void OnJumpForward(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(0.0f, 1.0f));
    }

    private void OnJumpBackward(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(0.0f, -1.0f));
    }

    private void OnJumpLeft(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(-1.0f, 0.0f));
    }

    private void OnJumpRight(InputValue input)
    {
        controlledCharacter.OnJumpInput(new Vector2(1.0f, 0.0f));
    }

    public void StartControlCharacter(PlayerCharacter newCharacter)
    {
        if (_ControlledCharacter == newCharacter) return;

        _ControlledCharacter = newCharacter;
        newCharacter.OnStartControlled(this);
    }
}
