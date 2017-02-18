using UnityEngine;
using System.Collections;
using MediaFlake.PhysicsCalculations;

public class PlayerMovementController : MonoBehaviour {
    public GameObject mainCamera { get; set; }
    private MovementEngine characterMovementEngine;
    private MovementModel characterMovementModel;
    private PhysicsHelper physicsHelper;
    private InputHandler inputHandler;
 
	void Start () {
        inputHandler = new InputHelper ();
        physicsHelper = new PhysicsHelper();

        /*
        * Set MovementModel variables
        */
        characterMovementModel = new MovementModel () {
            ForwardSpeed = 120.6f,
            SidewaySpeed = 5.2f,
            BackwardSpeed = 1.0f,
            RunFactor = 4f,
            JumpHeight = 3f,
            MaxSpeedTime = 2.5f,
            JumpAcceleration = 10f,
        };

        /*
        * Set MovementEngine variables and inject Movement Model
        */
        characterMovementEngine = new MovementEngine (characterMovementModel, physicsHelper, inputHandler) {
            ShellOffset = 0.1f,
            CheckGroundDistance = 0.01f,
            SlopeCurveModifier = new AnimationCurve (new Keyframe (-90.0f, 1.0f), new Keyframe (0.0f, 1.0f), new Keyframe (90.0f, 0.0f)),
            PlayerRigidbody = GetComponent<Rigidbody> (),
            PlayerCollider = GetComponent<CapsuleCollider> (),
            PlayerObject = this.gameObject,
            PlayerCamera = mainCamera,
        };
    }
	
	void FixedUpdate () {
        characterMovementEngine.PlayerTransform = this.gameObject.transform;
        characterMovementEngine.ObjectDirection = inputHandler.GetInput ();
        characterMovementEngine.SetMovement();
        characterMovementEngine.SetRotation();
        //characterMovementEngine.SetJump();
        Debug.Log(characterMovementEngine.PlayerRigidbody.velocity);
    }

}
