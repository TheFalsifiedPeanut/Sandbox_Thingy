namespace GeorgeProject
{
    using System;
    using UnityEngine;

    /// <summary>
    /// A class to handle player input.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        // xPosition is the horizontal axis for movement. For moving left and right.
        // yPostion is the vertical axis for movement. For moving forward and back.
        // xRotation is the mouse Y axis for camera looking. For looking up and down. Rotates camera.
        // yRotation is the mouse X axis for camera looking. For turning left and right. Rotates player.
        float xPosition, zPosition, xRotation, yRotation;
        // Jump is an action. I will cover actions more next time if you aren't familiar but for now think of it as a variable that can store a function in it.
        //We can then call the variable and it will call the function. This is the way we would normally handle events, in this situation whenever the Space key is pressed we call Jump.
        // This is often referred to as raising an event. Once Jump is called all functions that were previously assigned to it, or subscribed as it is commonly referred to as, will be called.
        
        Action Jump, ToggleUI, Pickup, StopPickup;
        // A reference to the player stats class.
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

        public void SubscribeToToggleUI (Action ToggleUI) { this.ToggleUI += ToggleUI; }

        public void UnSubscribeToToggleUI (Action ToggleUI) { this.ToggleUI -= ToggleUI; }

        public void SubscribeToPickup (Action Pickup) { this.Pickup += Pickup; }

        public void UnSubscribeToPickup (Action Pickup) { this.Pickup -= Pickup; }

        public void SubscribeToStopPickup (Action StopPickup) { this.StopPickup += StopPickup; }

        public void UnSubscribeToStopPickup (Action StopPickup) { this.StopPickup -= StopPickup; }

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
            OnToggleUI();
            OnPickup();
            OnStopPickup();
        }

        /// <summary>
        /// Checks if the Space key is pressed. If so it calls the jump action, thus raising the event.
        /// </summary>
        void OnJump()
        {
            // The Space key check for the action.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Calling the Jump action as if it was a normal function.
                Jump();
            }
        }
        private void OnToggleUI() {
            if(Input.GetKeyDown(KeyCode.Tab)) {
                ToggleUI();
            }
            
        }
        private void OnPickup() {
            if(Input.GetMouseButtonDown(0)) {
                Pickup();
            }
            
        }
        private void OnStopPickup() {
            if(Input.GetMouseButtonUp(0)) {
                StopPickup();
            }
        }
        
    }

    
    
}




