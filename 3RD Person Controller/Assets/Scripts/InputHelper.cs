using UnityEngine;
using System.Collections;

public class InputHelper : InputHandler {

   Vector2 NoUserInput = new Vector2 (0, 0);
   Vector2 SteadyForwardInput = new Vector2 (Input.GetAxis ("Horizontal"), 1.0f);

   public Vector2 GetInput () {
        if (GameManager.DisableInput) {
            return NoInput();
        }
        return new Vector2 {
            x = Input.GetAxis ("Horizontal"),
            y = 1.0f
        };
        
    }

    public Vector2 NoInput () {
        return NoUserInput;
    }
}
