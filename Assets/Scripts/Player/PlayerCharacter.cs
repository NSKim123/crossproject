using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerMovement _MovementComponent;

    private PlayerController _PlayerController;

    private PlayerCharacterAnimController _AnimController;

    private int _BackwardJumpCombo;

    public PlayerMovement movementComponent => (_MovementComponent ?? GetComponent<PlayerMovement>());

    public PlayerCharacterAnimController animController 
        => _AnimController ?? (_AnimController = GetComponentInChildren<PlayerCharacterAnimController>());

    public PlayerController controller => _PlayerController;

    public int backwardJumpCombo 
    {
        get
        {
            return _BackwardJumpCombo;
        }
        set
        {
            _BackwardJumpCombo = value;

            if (_BackwardJumpCombo >= 3)
                onGameOver?.Invoke();
        }
    }

    public event Action onGameOver;

    public void ResetPlayerCharacter()
    {
        movementComponent.ResetMovement();

        backwardJumpCombo = 0;
    }

    public void OnJumpInput(Vector2 inputValue)
    {
        movementComponent.OnJumpInput(inputValue);
    }

    public void OnStartControlled(PlayerController newController)
    {
        if (controller == newController) return;

        _PlayerController = newController;
    }
}
