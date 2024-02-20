using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 씬에 대한 컴포넌트입니다.
/// </summary>
public class GameSceneInstance : SceneInstance
{
    /// <summary>
    /// MapGenerator 컴포넌트를 가진 객체입니다.
    /// </summary>
    private MapGenerator _MapGenerator;

    [Header("# UI")]
    /// <summary>
    /// 게임 화면 씬의 UI Canvas 입니다.
    /// </summary>
    public GameSceneUI m_GameSceneUI;

    [Header("# 카메라 객체")]
    /// <summary>
    /// 게임 화면 씬의 카메라 객체입니다.
    /// </summary>
    public FollowCamera m_FollowCamera;

    protected override void Awake()
    {
        base.Awake();

        // MapGenerator 게임 오브젝트를 찾고, MapGenerator가 맵에 대한 ScriptableObject를 찾도록 설정해줍니다.
        InitMapGenerator();

        // 점프 이벤트를 바인드합니다.
        BindingJumpEvent();

        // 게임 오버 이벤트를 바인드합니다.
        BindingGameOverEvent();

        // FollowCamera 컴포넌트의 오프셋 값을 설정합니다.
        m_FollowCamera.SetPosOffset(
            m_FollowCamera.transform.position
            - playerController.controlledCharacter.transform.position) ;
    }

    private void Update()
    {
        // 카메라의 목표 위치를 설정합니다.
        m_FollowCamera.SetTargetPosition(playerController.controlledCharacter.transform.position);
    }

    /// <summary>
    /// MapGenerator 객체를 초기화합니다.
    /// </summary>
    private void InitMapGenerator()
    {
        // MapGenerator 게임 오브젝트를 찾습니다.
        _MapGenerator = FindObjectOfType<MapGenerator>() ?? (new GameObject("Object_MapGenerator")).AddComponent<MapGenerator>();

        // 찾아야하는 ScriptableObject 오브젝트의 경로를 설정합니다.
        _MapGenerator.SetScriptableObjectPath("ScriptableObject/MapTileData");
    }

    /// <summary>
    /// 점프 이벤트를 바인드합니다.
    /// </summary>
    private void BindingJumpEvent()
    {
        PlayerCharacter player = playerController.controlledCharacter;

        player.movementComponent.onJump += _MapGenerator.OnPlayerJump;
    }

    /// <summary>
    /// 게임 오버 이벤트를 바인드합니다.
    /// </summary>
    private void BindingGameOverEvent()
    {
        playerController.controlledCharacter.onGameOver += OnGameOver;
    }

    /// <summary>
    /// 게임을 시작(재시작 포함)하는 메서드입니다.
    /// </summary>
    public void InitGame()
    {
        // 맵 리셋
        _MapGenerator.ResetMap();

        // 캐릭터 리셋
        playerController.controlledCharacter.ResetPlayerCharacter();

        // 게임 오버 UI 비활성화
        m_GameSceneUI.m_gameOverPanel.gameObject.SetActive(false);

        // 시간 스케일 조정
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// 게임 오버 됐을 때 호출되는 메서드입니다.
    /// </summary>
    public void OnGameOver()
    {
        // 시간 스케일 조정
        Time.timeScale = 0.0f;

        // 게임 오버 UI 활성화
        m_GameSceneUI.m_gameOverPanel.gameObject.SetActive(true);
    }
}
