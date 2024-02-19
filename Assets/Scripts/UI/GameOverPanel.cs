using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [Header("# 게임 씬 인스턴스")]
    public GameSceneInstance m_GameSceneInstance;

    [Header("# 재시작 버튼")]
    public Button m_RestartButton;

    private void Awake()
    {
        m_RestartButton.onClick.AddListener(m_GameSceneInstance.InitGame);
    }
}
