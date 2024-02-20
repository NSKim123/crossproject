using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// ���� �����ϴ� ������ �ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class MapGenerator : MonoBehaviour
{
    /// <summary>
    /// �� Ÿ�� �����鿡 ���� ������ ����ִ� ScriptableObject �Դϴ�.
    /// </summary>
    private static MapTileScriptableObject m_MaptileScriptableObject;

    /// <summary>
    /// �ҷ��� MapTileScriptableObject �� ����Դϴ�.
    /// Resource Ŭ������ ���� �ҷ��� ���̹Ƿ� 'Resource/' ������ ��θ� �����մϴ�.
    /// </summary>
    private string _ScriptableObjectPath;

    /// <summary>
    /// �� �Դϴ�.
    /// </summary>
    public List<MapTile> m_Map = new List<MapTile>();

    private void Start()
    {
        // MapTileScriptableObject �� �ҷ��ɴϴ�.
        if (m_MaptileScriptableObject == null)
            m_MaptileScriptableObject = Resources.Load<MapTileScriptableObject>(_ScriptableObjectPath);

        // ���� �����մϴ�.
        InitMap();
    }

    /// <summary>
    /// ���� ó������ ������ �����ϴ� �޼����Դϴ�.
    /// </summary>
    private void InitMap()
    {
        // 21ĭ�� ������ Ÿ�Ϸ� �����մϴ�.
        for(int i = 0; i < 21; ++i)
        {
            CreateRandomTile(i);                  
        }
    }

    /// <summary>
    /// ������ Ÿ���� �ϳ� �����ϰ� m_Map�� �߰��մϴ�.
    /// </summary>
    /// <param name="tileIndex"> m_Map �� �� ��° ���ҷ� �߰������� �����մϴ�.</param>
    private void CreateRandomTile(int tileIndex)
    {
        // Ÿ�� ����(�ε���)�� �����ϰ� ���ϴ�.
        int randomTileTypeIndex = Random.Range(0, m_MaptileScriptableObject.m_MapTileDatas.Count);

        // �÷��̾ �������Ǵ� Ÿ���� ������ Ÿ�� ������ �����մϴ�.
        // (ó������ ���� ������ ���� �ʰ� �ϱ� ����)
        if(tileIndex == 10)
        {
            randomTileTypeIndex = 3;
        }

        // ���� �ε����� �������� ScriptableObject ���� �� Ÿ�� �������� ��θ� �޾ƿɴϴ�.
        string prefabPath = m_MaptileScriptableObject.GetPrefabPath(randomTileTypeIndex);

        // �� Ÿ�� �������� ��������մϴ�.
        MapTile tileObject = Instantiate(Resources.Load<MapTile>(prefabPath));

        // ������ Ÿ���� �ε����� ��ġ�� �����մϴ�.
        tileObject.SetTileIndex(tileIndex);
        tileObject.transform.position = Vector3.forward * (tileIndex - 10)*2;

        // m_Map�� �߰��մϴ�.
        m_Map.Insert(tileIndex, tileObject);        
    }

    /// <summary>
    /// �� Ÿ�� �ϳ��� �ı��մϴ�.
    /// </summary>
    /// <param name="tileIndex"> m_Map �� �� ��° ���Ҹ� �ı����� �����մϴ�.</param>
    private void DestroyTile(int tileIndex)
    {
        MapTile mapTile = m_Map[tileIndex];
        m_Map.RemoveAt(tileIndex);
        Destroy(mapTile.gameObject);
    }

    /// <summary>
    /// m_Map �� MapTile ���ҵ��� _TileIndex ����� �缳���մϴ�.
    /// </summary>
    private void SetTileIndices()
    {
        for(int i = 0; i < m_Map.Count; ++i)
        {
            m_Map[i].SetTileIndex(i);
        }
    }

    /// <summary>
    /// ���ο� ���� ����ϴ�.
    /// ������ �ִ� ���� �ı��ǰ� ���Ӱ� �����մϴ�.
    /// </summary>
    public void ResetMap()
    {
        for(int i = m_Map.Count - 1; i >= 0; --i)
        {
            Destroy(m_Map[i].gameObject);
            m_Map.RemoveAt(i);
        }
        m_Map.Clear();

        InitMap();
    }

    /// <summary>
    /// �÷��̾ ������ ������ �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    public void OnPlayerJumpForward()
    {
        DestroyTile(0);

        CreateRandomTile(20);

        SetTileIndices();
    }

    /// <summary>
    /// �÷��̾ �ڷ� ������ �� ȣ��Ǵ� �޼����Դϴ�.
    /// </summary>
    public void OnPlayerJumpBackward()
    {
        DestroyTile(20);

        CreateRandomTile(0);

        SetTileIndices();
    }

    /// <summary>
    /// ���� ���⿡ �°� OnPlayerJumpForward, OnPlayerJumpBackward �Լ��� ȣ���մϴ�.
    /// </summary>
    /// <param name="jumpDirection"> �÷��̾��� ���� ������ �����մϴ�. </param>
    public void OnPlayerJump(Vector2 jumpDirection)
    {
        // ������ ������ ��
        if (jumpDirection.y > 0.0f)
            OnPlayerJumpForward();
        // �ڷ� ������ ��
        else if (jumpDirection.y < 0.0f)
            OnPlayerJumpBackward();
    }

    /// <summary>
    /// ScriptableObject �� ��θ� �����մϴ�.
    /// </summary>
    /// <param name="path"> 'Resource/' ������ ��θ� �����ؾ��մϴ�. </param>
    public void SetScriptableObjectPath(string path)
    {
        _ScriptableObjectPath = path;
    }
}
