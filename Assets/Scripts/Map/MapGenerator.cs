using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private static MapTileScriptableObject m_MaptileScriptableObject;

    private string _ScriptableObjectPath;

    public List<MapTile> m_Map = new List<MapTile>();

    private void Start()
    {
        if (m_MaptileScriptableObject == null)
            m_MaptileScriptableObject = Resources.Load<MapTileScriptableObject>(_ScriptableObjectPath);

        InitMap();
    }

    private void InitMap()
    {
        for(int i = 0; i < 21; ++i)
        {
            CreateRandomTile(i);                  
        }
    }

    private void CreateRandomTile(int tileIndex)
    {
        int randomTileTypeIndex = Random.Range(0, 4);

        string prefabPath = m_MaptileScriptableObject.GetPrefabPath(randomTileTypeIndex);

        MapTile tileObject = Instantiate(Resources.Load<MapTile>(prefabPath));
        tileObject.SetTileIndex(tileIndex);
        tileObject.transform.position = Vector3.forward * (tileIndex - 10)*2;

        m_Map.Insert(tileIndex, tileObject);        
    }

    private void DestroyTile(int tileIndex)
    {
        MapTile mapTile = m_Map[tileIndex];
        m_Map.RemoveAt(tileIndex);
        Destroy(mapTile.gameObject);
    }

    private void SetTileIndices()
    {
        for(int i = 0; i < m_Map.Count; ++i)
        {
            m_Map[i].SetTileIndex(i);
        }
    }

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

    public void OnPlayerJumpForward()
    {
        DestroyTile(0);

        CreateRandomTile(20);

        SetTileIndices();
    }

    public void OnPlayerJumpBackward()
    {
        DestroyTile(20);

        CreateRandomTile(0);

        SetTileIndices();
    }

    public void SetScriptableObjectPath(string path)
    {
        _ScriptableObjectPath = path;
    }
}
