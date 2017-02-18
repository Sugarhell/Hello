using UnityEngine;
using System.Collections;
using MediaFlake.PhysicsCalculations;

/*
This class contains all the functions for the movement. 
*/
public class MovementEngine { 
    public float ShellOffset { get; set; } // The ammount of radius that the player will ignore during collision. Set to 0.1 for best results.
    public float CheckGroundDistance { get; set; } // The height from the ground that the player start checking for the ground distance. Set to 0.1 for best results.
    public Vector3 CurrentPosition { get; set; } 
    public Vector3 GroundContact { get; set; }
    public GameObject PlayerCamera { get; set; }
    public GameObject PlayerObject { get; set; }
    public Rigidbody PlayerRigidbody { get; set; }
    public Transform PlayerTransform { get; set; }
    public CapsuleCollider PlayerCollider { get; set; }
    public AnimationCurve SlopeCurveModifier { get; set; } // The animation curve for the slope movement
    public Vector2 ObjectDirection { get; set; }
    private Vector3 currentSpeed;
    private float targetAcceleration;
    private float jumpForce;
    private PhysicsHelper physicsFunctions;
    private MovementModel movementModel;
    private InputHandler inputHandler;

    public MovementEngine(MovementModel newMovementModel, PhysicsHelper newPhysicsCalculator, InputHandler newInputHandler) {
        movementModel = newMovementModel;
        physicsFunctions = newPhysicsCalculator;
        inputHandler = newInputHandler;
    }

    /*
    *Stores the User Input and returns it. 
    */
    public Vector2 GetUserInput() {
        Vector2 userInput = new Vector2 {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };
        return userInput;
    }
    
    /*
    *Check if the User is pressing any Axis Button and returns true or false. 
    */ 
    public bool IsMoving() {
        return (Mathf.Abs (ObjectDirection.x) > Mathf.Epsilon) || (Mathf.Abs (ObjectDirection.y) > Mathf.Epsilon);
    }

    /*
    *Set the current speed based on the user input and call the next function. 
    */
    public void SetMovement() {
        if (IsMoving()) {
            if (ObjectDirection.x > 0 || ObjectDirection.x < 0) {
                MoveSideway();
            }else if (ObjectDirection.y>0) {
                MoveForward();
            } 
        }
    }

    public void MoveSideway() {
        UpdatePosition(movementModel.SidewaySpeed);
    }

    public void MoveBackward() {
        UpdatePosition(movementModel.BackwardSpeed);
    }

    public void MoveForward() {
        UpdatePosition(movementModel.ForwardSpeed);
    }

    public void UpdatePosition (float targetVelocity) {
        targetAcceleration = physicsFunctions.CalculateAcceleration (Mathf.Abs(PlayerRigidbody.velocity.x + PlayerRigidbody.velocity.z), physicsFunctions.GetNetForce(movementModel.ForwardSpeed, movementModel.SidewaySpeed), movementModel.MaxSpeedTime);
        CurrentPosition = GetGroundSurface() * physicsFunctions.GetForce(PlayerRigidbody.mass, targetAcceleration, PlayerRigidbody.drag);
        SetMovementForce();
    }

    public Vector3 GetGroundSurface() {
        return Vector3.ProjectOnPlane (GetTransformPosition(), GetSurfaceType()).normalized;   
    }

    public Vector3 GetTransformPosition() {
        return PlayerTransform.forward + PlayerTransform.right * ObjectDirection.x;
    }

    public Vector3 GetSurfaceType() {
        RaycastHit raycastHitInfo;
        if (Physics.SphereCast (PlayerTransform.position, PlayerCollider.radius , -PlayerObject.transform.up, out raycastHitInfo,
                    50.0f, Physics.AllLayers)) {
            return raycastHitInfo.normal;
        } else {
            return raycastHitInfo.normal;
        }
    }

    public float FindSlopeAngle() { 
        return SlopeCurveModifier.Evaluate (Vector3.Angle (GetSurfaceType (), Vector3.up));
    }

    public void SetMovementForce() {
            PlayerRigidbody.AddForce(CurrentPosition, ForceMode.Impulse);
    }

    public void SetRotation() {
        if  (Mathf.Abs (Time.timeScale) > float.Epsilon) {
            if (Input.GetMouseButton(1)) {
                Quaternion playerRotation = Quaternion.Euler (0, PlayerCamera.transform.eulerAngles.y, 0);
                PlayerRigidbody.MoveRotation (Quaternion.Slerp (PlayerObject.transform.rotation, playerRotation, 0.1f));
            }
        }
    }

    public void SetJump() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if (IsGrounded()) {
                GetJumpForce ();
                PlayerRigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            }
        }
    }

    public bool IsGrounded() {
        RaycastHit outHit;
        float distanceFromGround = 0f;
        if(Physics.Raycast(PlayerObject.transform.position, -Vector3.up, out outHit, 50.0f)) {
            distanceFromGround = outHit.distance;
        }
        return ( distanceFromGround <= 1f && distanceFromGround > 0 );
    }

    public void GetJumpForce () {
        float jumpSpeed = physicsFunctions.GetInitialVelocity (0.0f, Physics.gravity.y, movementModel.JumpHeight);
        jumpForce = physicsFunctions.GetForceByDisplacement (PlayerRigidbody.mass, jumpSpeed, movementModel.JumpHeight);
    }

}
