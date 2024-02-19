using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{    
    private Camera _CameraComponent;

    private float _TargetPositionX;

    public float m_PosXOffset;

    private void Awake()
    {
        _CameraComponent = GetComponent<Camera>();
    }

    private void Update()
    {
        UpdatePositionX();
    }

    private void UpdatePositionX()
    {
        if (_TargetPositionX < Constants.MAPHORIZONTALLIMIT_MIN + 24.0f
            || _TargetPositionX > Constants.MAPHORIZONTALLIMIT_MAX - 24.0f)
            return;

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, _TargetPositionX + m_PosXOffset, 0.5f);
        transform.position = newPos;
    }

    public void SetTargetPositionX(float newTarget)
    {
        _TargetPositionX = newTarget;
    }
}
