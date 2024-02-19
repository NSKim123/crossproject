using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // 카메라의 오프셋을 설정합니다.
        m_FollowCamera.m_PosXOffset = m_FollowCamera.transform.position.x 
            - playerController.controlledCharacter.transform.position.x;
    }

    private void Update()
    {
        m_FollowCamera.SetTargetPositionX(playerController.controlledCharacter.transform.position.x);
    }

    private void InitMapGenerator()
    {
        _MapGenerator = FindObjectOfType<MapGenerator>() ?? (new GameObject("Object_MapGenerator")).AddComponent<MapGenerator>();

        _MapGenerator.SetScriptableObjectPath("ScriptableObject/MapTileData");
    }

    private void BindingJumpEvent()
    {
        PlayerCharacter player = playerController.controlledCharacter;
        player.movementComponent.onJump +=
            (Vector2 jumpDirection) =>
            {
                if (jumpDirection.y > 0.0f)
                    _MapGenerator.OnPlayerJumpForward();
                else if (jumpDirection.y < 0.0f)
                    _MapGenerator.OnPlayerJumpBackward();


                player.animController.SetTrigger(Constants.ANIMPARAM_ISJUMP);
            };

        player.movementComponent.onJump +=
            (Vector2 jumpDirection) =>
            {

                if (jumpDirection.y < 0.0f)
                    ++player.backwardJumpCombo;
                else
                    player.backwardJumpCombo = 0;
            };
    }

    private void BindingGameOverEvent()
    {
        playerController.controlledCharacter.onGameOver += OnGameOver;
    }

    public void InitGame()
    {
        _MapGenerator.ResetMap();

        playerController.controlledCharacter.ResetPlayerCharacter();

        m_GameSceneUI.m_gameOverPanel.gameObject.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void OnGameOver()
    {
        Time.timeScale = 0.0f;

        m_GameSceneUI.m_gameOverPanel.gameObject.SetActive(true);
    }
}
