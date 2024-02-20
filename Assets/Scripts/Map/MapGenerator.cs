using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 맵을 관리하는 역할을 하는 컴포넌트입니다.
/// </summary>
public class MapGenerator : MonoBehaviour
{
    /// <summary>
    /// 맵 타일 종류들에 대한 정보가 담겨있는 ScriptableObject 입니다.
    /// </summary>
    private static MapTileScriptableObject m_MaptileScriptableObject;

    /// <summary>
    /// 불러올 MapTileScriptableObject 의 경로입니다.
    /// Resource 클래스를 통해 불러올 것이므로 'Resource/' 이후의 경로를 저장합니다.
    /// </summary>
    private string _ScriptableObjectPath;

    /// <summary>
    /// 맵 입니다.
    /// </summary>
    public List<MapTile> m_Map = new List<MapTile>();

    private void Start()
    {
        // MapTileScriptableObject 를 불러옵니다.
        if (m_MaptileScriptableObject == null)
            m_MaptileScriptableObject = Resources.Load<MapTileScriptableObject>(_ScriptableObjectPath);

        // 맵을 생성합니다.
        InitMap();
    }

    /// <summary>
    /// 맵을 처음부터 끝까지 생성하는 메서드입니다.
    /// </summary>
    private void InitMap()
    {
        // 21칸을 랜덤한 타일로 생성합니다.
        for(int i = 0; i < 21; ++i)
        {
            CreateRandomTile(i);                  
        }
    }

    /// <summary>
    /// 랜덤한 타일을 하나 생성하고 m_Map에 추가합니다.
    /// </summary>
    /// <param name="tileIndex"> m_Map 의 몇 번째 원소로 추가할지를 전달합니다.</param>
    private void CreateRandomTile(int tileIndex)
    {
        // 타일 종류(인덱스)를 랜덤하게 고릅니다.
        int randomTileTypeIndex = Random.Range(0, m_MaptileScriptableObject.m_MapTileDatas.Count);

        // 플레이어가 리스폰되는 타일은 고정된 타일 종류를 생성합니다.
        // (처음부터 물에 빠지게 하지 않게 하기 위함)
        if(tileIndex == 10)
        {
            randomTileTypeIndex = 3;
        }

        // 뽑은 인덱스를 바탕으로 ScriptableObject 에서 맵 타일 프리팹의 경로를 받아옵니다.
        string prefabPath = m_MaptileScriptableObject.GetPrefabPath(randomTileTypeIndex);

        // 맵 타일 프리팹을 복사생성합니다.
        MapTile tileObject = Instantiate(Resources.Load<MapTile>(prefabPath));

        // 생성한 타일의 인덱스와 위치를 설정합니다.
        tileObject.SetTileIndex(tileIndex);
        tileObject.transform.position = Vector3.forward * (tileIndex - 10)*2;

        // m_Map에 추가합니다.
        m_Map.Insert(tileIndex, tileObject);        
    }

    /// <summary>
    /// 맵 타일 하나를 파괴합니다.
    /// </summary>
    /// <param name="tileIndex"> m_Map 의 몇 번째 원소를 파괴할지 전달합니다.</param>
    private void DestroyTile(int tileIndex)
    {
        MapTile mapTile = m_Map[tileIndex];
        m_Map.RemoveAt(tileIndex);
        Destroy(mapTile.gameObject);
    }

    /// <summary>
    /// m_Map 의 MapTile 원소들의 _TileIndex 멤버를 재설정합니다.
    /// </summary>
    private void SetTileIndices()
    {
        for(int i = 0; i < m_Map.Count; ++i)
        {
            m_Map[i].SetTileIndex(i);
        }
    }

    /// <summary>
    /// 새로운 맵을 만듭니다.
    /// 기존에 있던 맵은 파괴되고 새롭게 생성합니다.
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
    /// 플레이어가 앞으로 점프할 때 호출되는 메서드입니다.
    /// </summary>
    public void OnPlayerJumpForward()
    {
        DestroyTile(0);

        CreateRandomTile(20);

        SetTileIndices();
    }

    /// <summary>
    /// 플레이어가 뒤로 점프할 때 호출되는 메서드입니다.
    /// </summary>
    public void OnPlayerJumpBackward()
    {
        DestroyTile(20);

        CreateRandomTile(0);

        SetTileIndices();
    }

    /// <summary>
    /// 점프 방향에 맞게 OnPlayerJumpForward, OnPlayerJumpBackward 함수를 호출합니다.
    /// </summary>
    /// <param name="jumpDirection"> 플레이어의 점프 방향을 전달합니다. </param>
    public void OnPlayerJump(Vector2 jumpDirection)
    {
        // 앞으로 점프할 때
        if (jumpDirection.y > 0.0f)
            OnPlayerJumpForward();
        // 뒤로 점프할 때
        else if (jumpDirection.y < 0.0f)
            OnPlayerJumpBackward();
    }

    /// <summary>
    /// ScriptableObject 의 경로를 설정합니다.
    /// </summary>
    /// <param name="path"> 'Resource/' 이후의 경로를 전달해야합니다. </param>
    public void SetScriptableObjectPath(string path)
    {
        _ScriptableObjectPath = path;
    }
}
