namespace GeorgeProject
{
    using UnityEngine;

    /// <summary>
    /// A class to handle camera and player rotation.
    /// </summary>
    public class PlayerLook : MonoBehaviour
    {
        /// <summary>
        /// A reference to the camera. We could also get it in start using Camera.main, however if we use multiple cameras this could lead to issues.
        /// </summary>
        [SerializeField]
        Camera playerCamera;

        // A reference to the player stats class.
        PlayerStats playerStats;
        // A reference to the player inputs class.
        PlayerInput playerInput;

        void Start()
        {
            // Assign the Player Stats class.
            playerStats = GetComponent<PlayerStats>();
            // Assign the Player Inputs class.
            playerInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            // Call the looking.
            Look();
        }

        void Look()
        {
            // Get the x axis rotation. This is for looking up and down. Affects the camera.
            float xRotation = playerCamera.transform.rotation.eulerAngles.x - playerInput.GetXRotation();
            // Get the y axis rotation. This is for looking left and right. Affects the player.
            float yRotation = transform.rotation.eulerAngles.y + playerInput.GetYRotation();

            // This rescales the minimum angle to fix the negative issue we were having.
            float minimumAngleRescale = (360 + playerStats.GetMinimumAngle());
            // This mid point isn't totally necessary, it just finds the mid point between the minimum and maximum on the outside area (the area we are restricting).
            //This is just used for calculating if the camera is beyond the restrictions whether to snap it to the minimum or maximum.
            float midPoint = playerStats.GetMaximumAngle() + (minimumAngleRescale - playerStats.GetMaximumAngle()) * 0.5f;

            // This is the clamping for the minimum angle. It checks if the rotation lies out side the minimum and before or equal to the mid point.
            if (xRotation < minimumAngleRescale && xRotation >= midPoint)
            {
                // If the rotation lies outside the restriction clamp it back to the minimum.
                xRotation = playerStats.GetMinimumAngle();
            }

            // This is the clamping for the maximum angle. It checks if the rotation lies out side the maximum and after the mid point.
            if (xRotation > playerStats.GetMaximumAngle() && xRotation < midPoint)
            {
                // If the rotation lies outside the restriction clamp it back to the maximum.
                xRotation = playerStats.GetMaximumAngle();
            }

            // Set the rotation for the camera to be the x rotation, with necessary clamping.
            playerCamera.transform.rotation = Quaternion.Euler(xRotation, playerCamera.transform.rotation.eulerAngles.y, playerCamera.transform.rotation.eulerAngles.z);
            // Set the rotation for the player to be the y rotation.
            transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
        }
    }
}