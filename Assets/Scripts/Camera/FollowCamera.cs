using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라를 이동시키는 컴포넌트입니다.
/// </summary>
public class FollowCamera : MonoBehaviour
{    
    /// <summary>
    /// 목표 위치입니다.
    /// </summary>
    private Vector3 _TargetPosition;

    /// <summary>
    /// 위치에 대한 오프셋입니다.
    /// </summary>
    private Vector3 _PosOffset;

    private void Update()
    {
        // 위치를 갱신합니다.
        UpdatePositionX();
    }

    /// <summary>
    /// 위치를 갱신합니다.
    /// 카메라가 플레이어를 따라가지만, 맵 밖의 영역을 비추지 않도록 조정합니다.
    /// </summary>
    private void UpdatePositionX()
    {
        // 맵 영역을 벗어났다면
        if (_TargetPosition.x - _PosOffset.x < Constants.MAPHORIZONTALLIMIT_MIN + 24.0f
            || _TargetPosition.x - _PosOffset.x > Constants.MAPHORIZONTALLIMIT_MAX - 24.0f)
            return;

        // 위치를 조정합니다.
        Vector3 newPos = transform.position;
        newPos = Vector3.Lerp(newPos, _TargetPosition, 0.5f);
        transform.position = newPos;
    }

    /// <summary>
    /// 목표 위치를 설정합니다.
    /// </summary>
    /// <param name="newTarget"> 플레이어의 위치를 전달합니다. (오프셋이 고려되지 않은 값을 전달해야합니다.)</param>
    public void SetTargetPosition(Vector3 newTarget)
    {
        newTarget.y = 1.0f;
        _TargetPosition = newTarget + _PosOffset;
    }

    public void SetPosOffset(Vector3 newOffset)
    {
        _PosOffset = newOffset;
    }
}
