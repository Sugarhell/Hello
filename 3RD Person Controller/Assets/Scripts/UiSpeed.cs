using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSpeed : MonoBehaviour {

    public Rigidbody PlayerRigidBody;
    public Text KhmText;

    void Update () {
        KhmText.text = ( Mathf.Floor (( ( PlayerRigidBody.velocity.x + PlayerRigidBody.velocity.z ) * 18.0f ) / 5.0f) ).ToString ()+ "Km/h";
    }


    public float GetNetForce ( float Velocity1, float Velocity2 ) {
        return Mathf.Sqrt (Mathf.Pow (Velocity1, 2) + Mathf.Pow (Velocity2, 2));
    }

}
