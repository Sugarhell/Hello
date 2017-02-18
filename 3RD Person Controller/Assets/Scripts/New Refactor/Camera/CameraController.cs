using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    private CameraModel cameraModel;
    private CameraMovementEngine cameraMovementEngine;

    [Header("Target GameObject")]
    public GameObject FollowTarget;

    [Header("Camera Settings")]
    [Range(0f, 10f)]
    [Tooltip("The mouse sensitivity. 0 is the slowest and the 10 is the fastest")]
    public float MouseSensitivity = 2f;

    [Range (0.0f, 90f)]
    [Tooltip("The maximum and minimum angle that the camera can move to y axis")]
    public float CameraYAxisAngleLimit = 80.0f;

    [Range(0.5f, 40.0f)]
    [Tooltip("The default distance of the camera from the target")]
    float Cameradistance = 5.0f;

    [Range(0.0f, 1f)]
    [Tooltip("The minimum distance of the camera from the target")]
    public float MinCameraDistance = .6f;

    [Range(1f, 40.0f)]
    [Tooltip("The maximum distance of the camera from the target")]
    public float MaxCameraDistance = 20.0f;

    [Range(0, 200)]
    [Tooltip("The zoom rate of the camera")]
    int ZoomRate = 40;

    [Tooltip("Allow to move mouse to x axis")]
    public bool EnableMouseAxisXInput = true;

    [Tooltip("Allow to move mouse to y axis")]
    public bool EnableMouseAxisYInput = true;

    [Range(0.0f, 10.0f)]
    [Tooltip("How height difference of the camera with the player")]
    public float CameraTargetHeight = 1.7f;

    [Range(0.0f, 5.0f)]
    [Tooltip("The damping value for smoothing position and rotation")]
    public float SmoothDamping = 1.0f;

    [Tooltip("The distance from the wall when the camera collide and reposition ")]
    public float OffsetFromWall = 0.1f;

    [Tooltip("The collision layer mask that camera will collide with")]
    public LayerMask CollisionLayer = -1;

    void Start () {
        cameraModel = new CameraModel () {
            AxisYMinLimit = -CameraYAxisAngleLimit,
            AxisYMaxLimit = CameraYAxisAngleLimit,
            MouseSensitivity = MouseSensitivity * 100.0f,
            MouseSensitivityMultiplier = 0.02f,
            MinCameraDistance = MinCameraDistance,
            MaxCameraDistance = MaxCameraDistance,
            OffsetFromWalls = OffsetFromWall,
            MouseXEnabled = EnableMouseAxisXInput,
            MouseYEnabled = EnableMouseAxisYInput,
            DampingFactor = SmoothDamping,
            CameraTargetHeight = CameraTargetHeight,
            CollisionLayers = CollisionLayer,
            LockBehindTheTarget = false,
            ZoomRate = ZoomRate
        };

        cameraMovementEngine = new CameraMovementEngine(cameraModel) {
            CurrentDistance = Cameradistance,
            CorrectedDistance = Cameradistance,
            DesiredDistance = Cameradistance,
            FollowTarget = FollowTarget,
            CameraObject = this.gameObject,
            speedDistance = 5,
        };

	}
	
	void LateUpdate () {
        transform.LookAt(FollowTarget.transform.position);
        cameraMovementEngine.SetAxisRotation();
        cameraMovementEngine.ClampYAxisAngle();
        cameraMovementEngine.SetCameraRotation();
        cameraMovementEngine.SetCameraPosition();
	}
}
