using UnityEngine;
using System.Collections;

 namespace Globals
{

    #region Static Constants
    public static class PlayerInput
    {
        public static string Horizontal = "Horizontal";
        public static string Vertical   = "Vertical";
        public static string Fire1      = "Fire1";
        public static string Fire2      = "Fire2";
        public static string Fire3      = "Fire3";
        public static string Jump       = "Jump";
        public static string MouseX     = "Mouse X";
        public static string MouseY     = "Mouse Y";
        public static string Scroll     = "Mouse ScrollWheel";
        public static string Submit     = "Submit";
        public static string Cancel     = "Cancel";
        public static string Crouch     = "Crouch";
        public static string Run        = "Run";
    }
    #endregion

    public static class Function 
    {
        public static float AngleClamp(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }

            return Mathf.Clamp(angle, min, max);
        }
    }
}
