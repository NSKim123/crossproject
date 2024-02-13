using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerCharacter _ControlledCharacter;

    public PlayerCharacter controlledCharacter => _ControlledCharacter;

    private void OnMove(InputValue input)
    {
        float a = input.Get<float>();
        controlledCharacter.OnMoveInput((int)a);
    }

    private void OnJump(InputValue input)
    {
        float a = input.Get<float>();
        controlledCharacter.OnJumpInput((int)a);
    }

    public void StartControlCharacter(PlayerCharacter newCharacter)
    {
        if (_ControlledCharacter == newCharacter) return;

        _ControlledCharacter = newCharacter;
        newCharacter.OnStartControlled(this);
    }
}
