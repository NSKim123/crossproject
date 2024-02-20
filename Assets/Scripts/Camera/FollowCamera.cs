using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� �̵���Ű�� ������Ʈ�Դϴ�.
/// </summary>
public class FollowCamera : MonoBehaviour
{    
    /// <summary>
    /// ��ǥ ��ġ�Դϴ�.
    /// </summary>
    private Vector3 _TargetPosition;

    /// <summary>
    /// ��ġ�� ���� �������Դϴ�.
    /// </summary>
    private Vector3 _PosOffset;

    private void Update()
    {
        // ��ġ�� �����մϴ�.
        UpdatePositionX();
    }

    /// <summary>
    /// ��ġ�� �����մϴ�.
    /// ī�޶� �÷��̾ ��������, �� ���� ������ ������ �ʵ��� �����մϴ�.
    /// </summary>
    private void UpdatePositionX()
    {
        // �� ������ ����ٸ�
        if (_TargetPosition.x - _PosOffset.x < Constants.MAPHORIZONTALLIMIT_MIN + 24.0f
            || _TargetPosition.x - _PosOffset.x > Constants.MAPHORIZONTALLIMIT_MAX - 24.0f)
            return;

        // ��ġ�� �����մϴ�.
        Vector3 newPos = transform.position;
        newPos = Vector3.Lerp(newPos, _TargetPosition, 0.5f);
        transform.position = newPos;
    }

    /// <summary>
    /// ��ǥ ��ġ�� �����մϴ�.
    /// </summary>
    /// <param name="newTarget"> �÷��̾��� ��ġ�� �����մϴ�. (�������� ������� ���� ���� �����ؾ��մϴ�.)</param>
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
