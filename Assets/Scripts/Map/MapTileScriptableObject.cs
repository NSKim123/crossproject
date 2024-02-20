using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵을 구성하는 타일들의 종류에 대한 정보를 가지고 있는 스크립터블 오브젝트 클래스입니다.
/// </summary>
[CreateAssetMenu(fileName = "MapTileData", menuName = "ScriptableObject/MapTileData")]
public class MapTileScriptableObject : ScriptableObject
{
    /// <summary>
    /// 타일 데이터들을 담는 리스트입니다.
    /// </summary>
    public List<MapTileData> m_MapTileDatas = new();

    /// <summary>
    /// 타일 종류의 인덱스를 통해 타일 프리팹의 경로를 반환합니다.
    /// </summary>
    /// <param name="index"> 타일 종류의 인덱스를 전달합니다.</param>
    /// <returns> 인덱스에 대항하는 프리팹의 경로를 반환합니다. 'Resource/' 이후의 경로를 반환합니다.</returns>
    public string GetPrefabPath(int index)
    {
        return m_MapTileDatas.Find((MapTileData data) => (data.tileTypeIndex == index)).path;
    }
}

/// <summary>
/// 타일에 대한 정보를 나타내는 클래스입니다.
/// 타일 종류에 따른 인덱스와 그 타일 프리팹의 경로를 멤버로 가집니다.
/// </summary>
[System.Serializable]
public class MapTileData
{
    // 타일 종류에 따른 인덱스
    public int tileTypeIndex;

    // 타일 프리팹의 경로. ( 'Resource/' 이후의 경로 )
    public string path;
}
