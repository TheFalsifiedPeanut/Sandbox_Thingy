using UnityEngine;

/// <summary>
/// A class to handle player stats.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // Move Speed is an initial value for the move speed. I put this seperate so if we wish to modify the players speed we can always set it back to this when we are done.
    // Jump Height is an initial value for the jump height. It is seperate for the same reason as Move Speed.
    // Look Sensitivity X & Y is the axis sensitivity. Having X seperate from Y gives us more control.
    // Minimum & Maximum Angle are for the camera restraints.
    [SerializeField] float moveSpeed, jumpHeight, lookSensitivityX, lookSensitivityY, minimumAngle, maximumAngle;

    // Current Move Speed is an internal move speed that is used for calculations. This can be modified and set back using the above Move Speed initial value.
    // Current Jump Height is an internal jump height that is used for calculations. It has the same initial value capabilities as Current Move Speed.
    float currentMoveSpeed, currentJumpHeight;

    /// <summary>
    /// Get the current move speed.
    /// </summary>
    /// <returns> Returns the current move speed. </returns>
    public float GetCurrentMoveSpeed() { return currentMoveSpeed; }

    /// <summary>
    /// Get the current jump height.
    /// </summary>
    /// <returns> Returns the current jump height. </returns>
    public float GetCurrentJumpHeight() { return currentJumpHeight; }

    /// <summary>
    /// Get the X look sensitivity.
    /// </summary>
    /// <returns> Returns the X look sensitivity. </returns>
    public float GetLookSensitivityX() { return lookSensitivityX; }

    /// <summary>
    /// Get the Y look sensitivity.
    /// </summary>
    /// <returns> Returns the Y look sensitivity. </returns>
    public float GetLookSensitivityY() { return lookSensitivityY; }

    /// <summary>
    /// Get the minimum angle the camera can look down.
    /// </summary>
    /// <returns> Returns the minimum angle. </returns>
    public float GetMinimumAngle() { return minimumAngle; }

    /// <summary>
    /// Get the maximum angle the camera can look up.
    /// </summary>
    /// <returns> Returns the maximum angle. </returns>
    public float GetMaximumAngle() { return maximumAngle; }

    void Start()
    {
        // Set the intial values to the current ones for move speed and jump height.
        currentMoveSpeed = moveSpeed;
        currentJumpHeight = jumpHeight;
    }
}