using UnityEngine;
using System.Collections;

public class MovementModel { 
	public float ForwardSpeed { get; set; } //Default metric system is meter / second
    public float SidewaySpeed { get; set; }
    public float BackwardSpeed { get; set; }
    public float RunFactor { get; set; }
    public float JumpHeight { get; set; } // Set the height on meters
    public float MaxSpeedTime { get; set; } //The required time that the object needs to reach max velocity
    public float JumpAcceleration { get; set; }
}
