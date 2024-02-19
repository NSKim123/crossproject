using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [Header("# ���� �� �ν��Ͻ�")]
    public GameSceneInstance m_GameSceneInstance;

    [Header("# ����� ��ư")]
    public Button m_RestartButton;

    private void Awake()
    {
        m_RestartButton.onClick.AddListener(m_GameSceneInstance.InitGame);
    }
}
