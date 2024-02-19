using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInstance : SceneInstance
{
    /// <summary>
    /// MapGenerator ������Ʈ�� ���� ��ü�Դϴ�.
    /// </summary>
    private MapGenerator _MapGenerator;

    [Header("# UI")]
    /// <summary>
    /// ���� ȭ�� ���� UI Canvas �Դϴ�.
    /// </summary>
    public GameSceneUI m_GameSceneUI;

    [Header("# ī�޶� ��ü")]
    /// <summary>
    /// ���� ȭ�� ���� ī�޶� ��ü�Դϴ�.
    /// </summary>
    public FollowCamera m_FollowCamera;

    protected override void Awake()
    {
        base.Awake();

        // MapGenerator ���� ������Ʈ�� ã��, MapGenerator�� �ʿ� ���� ScriptableObject�� ã���� �������ݴϴ�.
        InitMapGenerator();

        // ���� �̺�Ʈ�� ���ε��մϴ�.
        BindingJumpEvent();

        // ���� ���� �̺�Ʈ�� ���ε��մϴ�.
        BindingGameOverEvent();

        // ī�޶��� �������� �����մϴ�.
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
