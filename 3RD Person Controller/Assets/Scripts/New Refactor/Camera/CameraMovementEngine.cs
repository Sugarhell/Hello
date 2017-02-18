using UnityEngine;
using System.Collections;
using Globals;

public class CameraMovementEngine {

    public GameObject CameraObject { get; set; }
    public GameObject FollowTarget { get; set; }
    public float speedDistance { get; set; }
    public float DesiredDistance { get; set; }
    public float CorrectedDistance { get; set; }
    public float CurrentDistance { get; set; }
    public bool RotateBehindTheObject { get; set; }
    private bool isCorrected;
    private Vector2 mouseInput;
    private Vector3 targetOffset;
    private Vector3 cameraPosition;
    private Quaternion cameraRotation;
    private float targetRotationAngle;
    private float currentRotationAngle;
    private CameraModel cameraModel;

    public CameraMovementEngine (CameraModel newCameraModel) {
        cameraModel = newCameraModel;
    }
    
    public void SetAxisRotation() {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0)) {
            if (cameraModel.MouseXEnabled) {
                mouseInput.x += (Input.GetAxis("Mouse X")) * SetMouseSensitivity();
            } else {
                RotateBehindTheTarget();
            }
            if (cameraModel.MouseYEnabled) {
                mouseInput.y -= (Input.GetAxis("Mouse Y")) * SetMouseSensitivity();
            }
        } else if (Input.GetAxis("Horizontal") != 0  || (Input.GetAxis("Vertical") != 0 || RotateBehindTheObject)) {
            RotateBehindTheTarget();
        }
    }

    public float SetMouseSensitivity() {
        return cameraModel.MouseSensitivity * cameraModel.MouseSensitivityMultiplier;
    }

    public Vector2 GetMouseInput() {
        Vector2 userMouseInput = new Vector2 {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };
        return userMouseInput;
    }

    public void RotateBehindTheTarget() {
        targetRotationAngle = FollowTarget.transform.eulerAngles.y;
        currentRotationAngle = CameraObject.transform.eulerAngles.y;
        mouseInput.x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, cameraModel.DampingFactor );
        RotateBehindTheObject = targetRotationAngle != currentRotationAngle;
    }

    public void ClampYAxisAngle() {
        mouseInput.y = Function.AngleClamp(mouseInput.y, cameraModel.AxisYMinLimit, cameraModel.AxisYMaxLimit);
    }

    public Quaternion GetCameraQuaterion() {
        cameraRotation = Quaternion.Euler(mouseInput.y, mouseInput.x, 0);
        return cameraRotation;
    }

    public void SetCameraRotation() {
        CameraObject.transform.rotation = Quaternion.SlerpUnclamped(CameraObject.transform.rotation, GetCameraQuaterion(), cameraModel.DampingFactor);
    }

    public float GetZoomRate() {
        return cameraModel.ZoomRate * speedDistance * Time.deltaTime;
    }

    public float GetClampedDistance (float distance) {
        return Mathf.Clamp(distance, cameraModel.MinCameraDistance, cameraModel.MaxCameraDistance);
    }

    public void SetCameraPosition() {
        SetCorrectedDistance();
        FixCameraOnCollision();
        CameraObject.transform.position = Vector3.Slerp(CameraObject.transform.position, cameraPosition, cameraModel.DampingFactor);  
    }

    public void SetCorrectedDistance() {
        DesiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Mathf.Abs(DesiredDistance) * GetZoomRate();
        CorrectedDistance = GetClampedDistance(DesiredDistance);
        SetCameraOffset();
    }

    public void SetCameraOffset() {
        targetOffset = new Vector3 (0, -cameraModel.CameraTargetHeight, 0);
        SetCameraDistance(DesiredDistance);
    }

    public void SetCurrentDistance() {
        CurrentDistance = !isCorrected || CorrectedDistance > CurrentDistance ? Mathf.Lerp(CurrentDistance, CorrectedDistance, Time.deltaTime * cameraModel.DampingFactor) : CorrectedDistance;
        CurrentDistance = GetClampedDistance(CurrentDistance);
        SetCameraDistance(CurrentDistance);
    }

    public void SetCameraDistance (float distance) {
        cameraPosition = FollowTarget.transform.position - (cameraRotation * Vector3.forward * distance + targetOffset);
    }

    /*
     * Correct camera position on collision, by setting a safety offset distance from the colliding object
     * Offset prevents partially clipping by the camera's front clipping plane
     * 
    */
    public void FixCameraOnCollision() {
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(FollowTarget.transform.position.x, FollowTarget.transform.position.y, FollowTarget.transform.position.z) - targetOffset;
        isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, CameraObject.transform.position, out collisionHit, cameraModel.CollisionLayers.value)) {
            CorrectedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - cameraModel.OffsetFromWalls;
            isCorrected = true;
        }
        SetCurrentDistance();
    }
}
