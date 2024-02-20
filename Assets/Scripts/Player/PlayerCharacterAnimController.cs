using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터의 애니메이션을 조절하는 컴포넌트입니다.
/// </summary>
public class PlayerCharacterAnimController : MonoBehaviour
{
    /// <summary>
    /// 애니메이터 컴포넌트입니다.
    /// </summary>
    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Trigger 형식의 애니메이션 파라미터를 설정합니다.
    /// </summary>
    /// <param name="param"> 애니메이션 파라미터 이름을 전달합니다. </param>
    public void SetTrigger(string param)
    {
        _Animator.SetTrigger(param);
    }
}
