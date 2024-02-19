using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimController : MonoBehaviour
{
    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    public void SetTrigger(string param)
    {
        _Animator.SetTrigger(param);
    }
}
