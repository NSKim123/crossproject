using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˽� ���� ���� ��Ű�� ������Ʈ�� ���� ������Ʈ�Դϴ�.
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
