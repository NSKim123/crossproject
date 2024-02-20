using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� �� Ȱ��ȭ�� Panel�� ������Ʈ�Դϴ�.
/// </summary>
public class GameOverPanel : MonoBehaviour
{    
    [Header("# ���� �� �ν��Ͻ�")]
    public GameSceneInstance m_GameSceneInstance;

    [Header("# ����� ��ư")]
    public Button m_RestartButton;

    private void Awake()
    {
        // ��ư Ŭ�� �̺�Ʈ ���ε�
        m_RestartButton.onClick.AddListener(m_GameSceneInstance.InitGame);
    }
}
