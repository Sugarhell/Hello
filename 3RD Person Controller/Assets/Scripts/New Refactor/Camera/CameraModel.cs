using UnityEngine;
using System.Collections;

public class CameraModel  {
    public float MouseSensitivity { get; set; }
    public float MouseSensitivityMultiplier { get; set; }
    public float DampingFactor { get; set; }
    public float AxisYMinLimit { get; set; }
    public float AxisYMaxLimit { get; set; }
    public float ZoomRate { get; set; }
    public float MinCameraDistance { get; set; }
    public float MaxCameraDistance { get; set; }
    public float CameraTargetHeight { get; set; }
    public float CameraDistance { get; set; }
    public float OffsetFromWalls { get; set; }
    public bool LockBehindTheTarget { get; set; }
    public bool MouseXEnabled { get; set; }
    public bool MouseYEnabled { get; set; }
    public LayerMask CollisionLayers { get; set; }
}
