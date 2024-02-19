using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    private int _TileIndex;

    private void FixedUpdate()
    {
        SetTilePosition();
    }

    private void SetTilePosition()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = Vector3.forward * ((_TileIndex - 10) * 2);

        transform.position = Vector3.Lerp(currentPos, targetPos, 0.1f);
    }

    public void SetTileIndex(int index)
    {
        _TileIndex = index;
    }
}
