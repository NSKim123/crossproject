using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimController : MonoBehaviour
{
    /// <summary>
    /// �ִϸ����� ������Ʈ�Դϴ�.
    /// </summary>
    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Trigger ������ �ִϸ��̼� �Ķ���͸� �����մϴ�.
    /// </summary>
    /// <param name="param"> �ִϸ��̼� �Ķ���� �̸��� �����մϴ�. </param>
    public void SetTrigger(string param)
    {
        _Animator.SetTrigger(param);
    }
}
