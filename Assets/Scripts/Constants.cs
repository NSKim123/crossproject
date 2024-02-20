using System;
using UnityEngine;

/// <summary>
/// 상수들을 저장해둔 정적 클래스입니다.
/// </summary>
public static class Constants
{
    /// <summary>
    /// 애니메이션 파라미터 이름
    /// </summary>
    public const string ANIMPARAM_ISJUMP = "jump";
    public const string ANIMPARAM_RANDOMVALUE = "_RandomValue";

    /// <summary>
    /// 레이어 이름
    /// </summary>
    public const string LAYERNAME_GROUND = "Ground";
    public const string LAYERNAME_OBSTACLE = "Obstacle";

    /// <summary>
    /// 맵의 x 축 경계값을 나타냅니다.
    /// </summary>
    public const float MAPHORIZONTALLIMIT_MIN = -48.5f;
    public const float MAPHORIZONTALLIMIT_MAX = 48.5f;    
}