using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ���� ������Ʈ�Դϴ�.
/// </summary>
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

        // FollowCamera ������Ʈ�� ������ ���� �����մϴ�.
        m_FollowCamera.SetPosOffset(
            m_FollowCamera.transform.position
            - playerController.controlledCharacter.transform.position) ;
    }

    private void Update()
    {
        // ī�޶��� ��ǥ ��ġ�� �����մϴ�.
        m_FollowCamera.SetTargetPosition(playerController.controlledCharacter.transform.position);
    }

    /// <summary>
    /// MapGenerator ��ü�� �ʱ�ȭ�մϴ�.
    /// </summary>
    private void InitMapGenerator()
    {
        // MapGenerator ���� ������Ʈ�� ã���ϴ�.
        _MapGenerator = FindObjectOfType<MapGenerator>() ?? (new GameObject("Object_MapGenerator")).AddComponent<MapGenerator>();

        // ã�ƾ��ϴ� ScriptableObject ������Ʈ�� ��θ� �����մϴ�.
        _MapGenerator.SetScriptableObjectPath("ScriptableObject/MapTileData");
    }

    /// <summary>
    /// ���� �̺�Ʈ�� ���ε��մϴ�.
    /// </summary>
    private void BindingJumpEvent()
    {
        PlayerCharacter player = playerController.controlledCharacter;

        player.movementComponent.onJump += _MapGenerator.OnPlayerJump;
    }

    /// <summary>
    /// ���� ���� �̺�Ʈ�� ���ε��մϴ�.
    /// </summary>
    private void BindingGameOverEvent()
    {
        playerController.controlledCharacter.onGameOver += OnGameOver;
    }

    /// <summary>
    /// ������ ����(����� ����)�ϴ� �޼����Դϴ�.
    /// </summary>
    public void InitGame()
    {
        // �� ����
        _MapGenerator.ResetMap();

        // ĳ���� ����
        playerController.controlledCharacter.ResetPlayerCharacter();

        // ���� ���� UI ��Ȱ��ȭ
        m_GameSceneUI.m_gameOverPanel.gameObject.SetActive(false);

        // �ð� ������ ����
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// ���� ���� ���� �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    public void OnGameOver()
    {
        // �ð� ������ ����
        Time.timeScale = 0.0f;

        // ���� ���� UI Ȱ��ȭ
        m_GameSceneUI.m_gameOverPanel.gameObject.SetActive(true);
    }
}
