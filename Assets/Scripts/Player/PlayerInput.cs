    using System;
    using UnityEngine;

/// <summary>
/// A class to handle player input.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    private bool LockedInteraction;
    // xPosition is the horizontal axis for movement. For moving left and right.
    // yPostion is the vertical axis for movement. For moving forward and back.
    // xRotation is the mouse Y axis for camera looking. For looking up and down. Rotates camera.
    // yRotation is the mouse X axis for camera looking. For turning left and right. Rotates player.
    float xPosition, zPosition, xRotation, yRotation;
    // The following are actions that act as a way of storing functions and calling them all when some condition is met. We can call the variable and it will call the function or functions subscribed.
    // This is the way we would normally handle events, for example whenever the Space key is pressed we call Jump. This is often referred to as raising an event.
    // Once Jump is called all functions that were previously assigned to it, or subscribed, as it is commonly referred to as, will be called.
    Action Jump, ToggleUI, Interact, StopInteract;
    // A reference to the Player Stats class.
    PlayerStats playerStats;

    /// <summary>
    /// Subscribe to the Jump action. Passing in a function as a paramter will add it to the Jump action. When the Jump action is called it will run all functions that we subscribed.
    /// </summary>
    /// <param name="Jump"> The function to subscribe. </param>
    public void SubscribeToJump(Action Jump) { this.Jump += Jump; }

    /// <summary>
    /// Unubscribe to the Jump action. Passing in a function as a paramter will remove it from the Jump action. When the Jump action is called it will no longer run this functions that we unsubscribed.
    /// </summary>
    /// <param name="Jump"> The function to unsubscribe. </param>
    public void UnsubscribeToJump(Action Jump) { this.Jump -= Jump; }

    /// <summary>
    /// Subscribe to the Toggle UI action.
    /// </summary>
    /// <param name="ToggleUI"> The function to subscribe. </param>
    public void SubscribeToToggleUI(Action ToggleUI) { this.ToggleUI += ToggleUI; }

    /// <summary>
    /// Unsubscribe to the Toggle UI action.
    /// </summary>
    /// <param name="ToggleUI"> The function to unsubscribe. </param>
    public void UnSubscribeToToggleUI(Action ToggleUI) { this.ToggleUI -= ToggleUI; }

    /// <summary>
    /// Subscribe to the OnInteract action.
    /// </summary>
    /// <param name="Interact"> The function to subscribe. </param>
    public void SubscribeToInteract(Action Interact) { this.Interact += Interact; }

    /// <summary>
    /// Unsubscribe to the Interact action.
    /// </summary>
    /// <param name="Interact"> The function to unsubscribe. </param>
    public void UnSubscribeToInteract(Action Interact) { this.Interact -= Interact; }

    /// <summary>
    /// Subscribe to the StopInteract action.
    /// </summary>
    /// <param name="StopInteract"> The function to subscribe. </param>
    public void SubscribeToStopInteract(Action StopInteract) { this.StopInteract += StopInteract; }

    /// <summary>
    /// Unsubscribe to the StopInteract action.
    /// </summary>
    /// <param name="StopInteract"> The function to unsubscribe. </param>
    public void UnSubscribeToStopInteract(Action StopInteract) { this.StopInteract -= StopInteract; }
    public void ToggleLock()
    {
        LockedInteraction = LockedInteraction ? false : true;
    }
    /// <summary>
    /// Get the x axis movement. For moving left and right.
    /// </summary>
    /// <returns> Returns the x axis movement. </returns>
    public float GetXPosition() { return xPosition; }

    /// <summary>
    /// Get the z axis movement. For moving forward and back.
    /// </summary>
    /// <returns> Returns the z axis movement. </returns>
    public float GetZPosition() { return zPosition; }

    /// <summary>
    /// Get the x axis rotation. For looking up and down.
    /// </summary>
    /// <returns> Returns the x axis rotation. </returns>
    public float GetXRotation() { return xRotation; }

    /// <summary>
    /// Get the y axis rotation. For turning left and right.
    /// </summary>
    /// <returns> Returns the y axis rotation. </returns>
    public float GetYRotation() { return yRotation; }

    void Start()
    {
        LockedInteraction = true;
        // Assign the player stats class.
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Checking for inputs.
        Inputs();
    }

    void Inputs()
    {
        if(!LockedInteraction)
        {
            // The x axis movement amount.
            xPosition = Input.GetAxis("Horizontal");
            // The y axis movement amount.
            zPosition = Input.GetAxis("Vertical");

            // The x axis rotation amount multiplied by the sensitivity for that axis.
            xRotation = Input.GetAxis("Mouse Y") * playerStats.GetLookSensitivityX();
            // The y axis rotation amount multiplied by the sensitivity for that axis.
            yRotation = Input.GetAxis("Mouse X") * playerStats.GetLookSensitivityY();

            // Check for jumping.
            OnJump();
            // Check for interact.
            OnInteract();
            // Check for stop interact.
            OnStopInteract();
        }
        OnToggleUI();
    }

    /// <summary>
    /// Checks if the Space key is pressed. If so it calls the jump action, thus raising the event.
    /// </summary>
    void OnJump()
    {
        // The Space key check for the action.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check the Jump action is not null.
            if (Jump != null)
            {
                // Calling the Jump action as if it was a normal function.
                Jump();
            }
        }
    }

    /// <summary>
    /// Checks if the Tab key is pressed. If so it toggles the state of the UI.
    /// </summary>
    void OnToggleUI()
    {
        // The Tab key check for the action.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Check the ToggleUI action is not null.
            if (ToggleUI != null)
            {
                // Calling the ToggleUI action.
                ToggleUI();
                xPosition = 0;
                zPosition = 0;
                xRotation = 0;
                yRotation = 0;
            }
        }

    }

    /// <summary>
    /// Checks if the Left Mouse Button is pressed. If so it fires the interaction event.
    /// </summary>
    private void OnInteract()
    {
        // The Left Mouse pressed check for the action.
        if (Input.GetMouseButtonDown(0))
        {
            // Check the Interact action is not null.
            if (Interact != null)
            {
                // Calling the Interact action.
                Interact();
            }
        }

    }

    /// <summary>
    /// Checks if the Left Mouse Button is released. If so it fires the stop interaction event.
    /// </summary>
    private void OnStopInteract()
    {
        // The Left Mouse release check for the action.
        if (Input.GetMouseButtonUp(0))
        {
            // Check the StopInteract action is not null.
            if (StopInteract != null)
            {
                // Calling the StopInteract action.
                StopInteract();
            }
        }
    }
}