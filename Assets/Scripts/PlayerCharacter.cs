using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerMovement _MovementComponent;

    private PlayerController _Controller;

    public PlayerMovement movementComponent => (_MovementComponent ?? GetComponent<PlayerMovement>());

    public PlayerController controller => _Controller;

    public void OnMoveInput(int a)
    {
        movementComponent.OnMoveInput((int)a);
    }

    public void OnJumpInput(int a)
    {
        movementComponent.OnJumpInput((int)a);
    }

    public void OnStartControlled(PlayerController newController)
    {
        if (controller == newController) return;

        _Controller = newController;
    }
}
