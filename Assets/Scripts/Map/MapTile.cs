using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �����ϴ� �ϳ��� Ÿ�Ͽ� ���� ������Ʈ�Դϴ�.
/// </summary>
public class MapTile : MonoBehaviour
{
    /// <summary>
    /// �� Ÿ���� �� ��° �ε����� ���������� ��Ÿ���ϴ�.
    /// �� ��° �ε����� ���������� ���� ��ġ�� �������ϴ�.
    /// </summary>
    private int _TileIndex;

    private void Awake()
    {
        TryGetComponent<Animator>(out Animator animator);
        animator?.SetFloat(Constants.ANIMPARAM_RANDOMVALUE, Random.Range(0.5f, 1.0f));
    }

    private void FixedUpdate()
    {
        // ��ġ�� �����մϴ�.
        SetTilePosition();
    }

    /// <summary>
    /// ��ġ�� �����մϴ�.
    /// �� Ÿ���� _TileIndex ���� ���� ��ġ�� �����մϴ�.
    /// </summary>
    private void SetTilePosition()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = Vector3.forward * ((_TileIndex - 10) * 2);

        transform.position = Vector3.Lerp(currentPos, targetPos, 0.1f);
    }

    /// <summary>
    /// _TileIndex ���� �����մϴ�.
    /// </summary>
    /// <param name="newIndex"> ������ ���� �����մϴ�.</param>
    public void SetTileIndex(int newIndex)
    {
        _TileIndex = newIndex;
    }
}
