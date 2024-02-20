using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 오버 시 활성화될 Panel의 컴포넌트입니다.
/// </summary>
public class GameOverPanel : MonoBehaviour
{    
    [Header("# 게임 씬 인스턴스")]
    public GameSceneInstance m_GameSceneInstance;

    [Header("# 재시작 버튼")]
    public Button m_RestartButton;

    private void Awake()
    {
        // 버튼 클릭 이벤트 바인드
        m_RestartButton.onClick.AddListener(m_GameSceneInstance.InitGame);
    }
}
