using UnityEngine;
using System.Collections;


namespace MediaFlake.PhysicsCalculations {

    public class PhysicsHelper {

        public float GetInitialVelocity ( float FinalVelocity, float Acceleration, float Displacement ) {
            return ( FinalVelocity * FinalVelocity ) - 2f * Acceleration * Displacement;
        }

        public float GetAcceleration ( float initialVelocity, float finalVelocity, float time ) {
            return ( finalVelocity - initialVelocity ) / time;
        }

        public float GetForce ( float mass, float acceleration, float drag ) {
            return mass * acceleration ;
        }

        public float GetForce ( float mass, float acceleration ) {
            return mass * acceleration;
        }

        public float GetForceByDisplacement ( float mass, float initialVelocity, float displacement ) {
            return ( ( mass * initialVelocity ) / 2 ) / displacement;
        }

        public float GetDistance ( float acceleration, float time ) {
            return 1 / 2 * acceleration * ( Mathf.Pow (time, 2.0f) );
        }

        public float GetDistanceByVelocity ( float velocity, float time ) {
            return 1 / 2 * velocity * time;
        }

        public float GetDistanceByAcceleration ( float velocity, float acceleration ) {
            return ( 1 / 2 * ( Mathf.Pow (velocity, 2f) ) / acceleration );
        }

        public float GetNetForce ( float Velocity1, float Velocity2 ) {
            return Mathf.Sqrt (Mathf.Pow (Velocity1, 2) + Mathf.Pow (Velocity2, 2));
        }

        public float CalculateAcceleration ( float currentVelocity, float maxVelocity, float maxAcceleration ) {
            if (currentVelocity > maxVelocity / 4) {
                return maxAcceleration;
            } else if (currentVelocity > maxVelocity / 3) {
                return maxAcceleration / 4;
            } else if (currentVelocity > maxVelocity / 2) {
                return maxAcceleration * 1 / 2;
            }
            return maxAcceleration;
        }
        
    
        }
    }

