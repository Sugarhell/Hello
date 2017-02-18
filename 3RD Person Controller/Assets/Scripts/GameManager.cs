using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool DisableInput = true;
    public static bool Loading;
    public static bool ReloadLevel; 

    public bool disableInput {
        set {
            DisableInput = value;
        }
    }
}
