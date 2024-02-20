using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 접촉시 게임 오버 시키는 오브젝트에 대한 컴포넌트입니다.
/// </summary>
public class DangerousObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerCharacter>(out PlayerCharacter player))
        {
            player.OnGameOver();
        }
    }
}
