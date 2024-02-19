using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTileData", menuName = "ScriptableObject/MapTileData")]
public class MapTileScriptableObject : ScriptableObject
{
    public List<MapTileData> m_MapTileDatas = new();

    public string GetPrefabPath(int index)
    {
        return m_MapTileDatas.Find((MapTileData data) => (data.tileTypeIndex == index)).path;
    }
}

[System.Serializable]
public class MapTileData
{
    public int tileTypeIndex;
    public string path;
}
