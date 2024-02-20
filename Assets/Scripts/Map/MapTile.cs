using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵을 구성하는 하나의 타일에 대한 컴포넌트입니다.
/// </summary>
public class MapTile : MonoBehaviour
{
    /// <summary>
    /// 이 타일이 몇 번째 인덱스를 가지는지를 나타냅니다.
    /// 몇 번째 인덱스를 가지는지에 따라 위치가 정해집니다.
    /// </summary>
    private int _TileIndex;

    private void Awake()
    {
        TryGetComponent<Animator>(out Animator animator);
        animator?.SetFloat(Constants.ANIMPARAM_RANDOMVALUE, Random.Range(0.5f, 1.0f));
    }

    private void FixedUpdate()
    {
        // 위치를 조정합니다.
        SetTilePosition();
    }

    /// <summary>
    /// 위치를 조정합니다.
    /// 이 타일의 _TileIndex 값에 따라 위치를 설정합니다.
    /// </summary>
    private void SetTilePosition()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = Vector3.forward * ((_TileIndex - 10) * 2);

        transform.position = Vector3.Lerp(currentPos, targetPos, 0.1f);
    }

    /// <summary>
    /// _TileIndex 값을 설정합니다.
    /// </summary>
    /// <param name="newIndex"> 설정할 값을 전달합니다.</param>
    public void SetTileIndex(int newIndex)
    {
        _TileIndex = newIndex;
    }
}
