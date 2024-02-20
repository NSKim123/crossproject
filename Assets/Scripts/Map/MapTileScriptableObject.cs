using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �����ϴ� Ÿ�ϵ��� ������ ���� ������ ������ �ִ� ��ũ���ͺ� ������Ʈ Ŭ�����Դϴ�.
/// </summary>
[CreateAssetMenu(fileName = "MapTileData", menuName = "ScriptableObject/MapTileData")]
public class MapTileScriptableObject : ScriptableObject
{
    /// <summary>
    /// Ÿ�� �����͵��� ��� ����Ʈ�Դϴ�.
    /// </summary>
    public List<MapTileData> m_MapTileDatas = new();

    /// <summary>
    /// Ÿ�� ������ �ε����� ���� Ÿ�� �������� ��θ� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="index"> Ÿ�� ������ �ε����� �����մϴ�.</param>
    /// <returns> �ε����� �����ϴ� �������� ��θ� ��ȯ�մϴ�. 'Resource/' ������ ��θ� ��ȯ�մϴ�.</returns>
    public string GetPrefabPath(int index)
    {
        return m_MapTileDatas.Find((MapTileData data) => (data.tileTypeIndex == index)).path;
    }
}

/// <summary>
/// Ÿ�Ͽ� ���� ������ ��Ÿ���� Ŭ�����Դϴ�.
/// Ÿ�� ������ ���� �ε����� �� Ÿ�� �������� ��θ� ����� �����ϴ�.
/// </summary>
[System.Serializable]
public class MapTileData
{
    // Ÿ�� ������ ���� �ε���
    public int tileTypeIndex;

    // Ÿ�� �������� ���. ( 'Resource/' ������ ��� )
    public string path;
}
