using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script used for interactions that concerns objects.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The current tool.
    /// </summary>
    [SerializeField] PlayerTool playerTool;
    /// <summary>
    /// The Interaction Search for finding all potential interactable objects.
    /// </summary>
    [SerializeField] PlayerInteractionSearch playerInteractionSearch;
    /// <summary>
    /// The current Interaction Target.
    /// </summary>
    [SerializeField] GameObject interactionTarget;
    /// <summary>
    /// Specifies an interaction layer.
    /// </summary>
    [SerializeField] LayerMask layer;

    /// <summary>
    /// A reference to the PlayerInput script.
    /// </summary>
    PlayerInput playerInput;
    /// <summary>
    /// A reference to the PlayerInventory script.
    /// </summary>
    PlayerInventory playerInventory;

    /// <summary>
    /// Turns the animator on and off
    /// </summary>
    bool animatorStatus;
    #endregion

    #region Getters
    /// <summary>
    /// Get the current Interaction Target.
    /// </summary>
    /// <returns> Returns the Interaction Target. </returns>
    public GameObject GetInteractionTarget() 
    { 
        return interactionTarget; 
    }
    #endregion

    #region Setters
    /// <summary>
    /// Sets the current Interaction Target to the specified object.
    /// </summary>
    /// <param name="interactionTarget"> The object to set. </param>
    public void SetInteractionTarget(GameObject interactionTarget) { this.interactionTarget = interactionTarget; }
    #endregion

    void Start()
    {
        //Fetches PlayerInput and asignes it to PlayerInput.
        playerInput = GetComponent<PlayerInput>();
        //Fetches PlayerInventory and asignes it to playerInventory.
        playerInventory = GetComponent<PlayerInventory>();
        //Listens for interaction events. Activates OnInteract when an events happens.
        playerInput.SubscribeToInteract(OnInteract);


        //playerInput.SubscribeToStopInteract(OnStopInteract);
    }
    private void Update()
    {
        AnimatorUpdate();
    }
    void AnimatorUpdate()
    {
        //Checks the animator's status for if it is true
        if (animatorStatus)
        {
            //enables the animator.
            playerTool.EnableAnimator();
            //Turns the animatior status back to false.
            animatorStatus = false;
        }
    }

    /// <summary>
    /// When the player is clicking to interact.
    /// </summary>
    public void OnInteract()
    {
        //Checks if the object you are interacting with is null.
        if (interactionTarget != null)
        {
            //Uses the current tool.
            playerTool.Interact(interactionTarget);
        }
    }

    /// <summary>
    /// When the player ends interacting.
    /// </summary>
    public void OnStopInteract()
    {
        //Runs the StopInteract function.
        playerTool.StopInteract();
    }
    /// <summary>
    /// A function used to set current tools.
    /// </summary>
    /// <param name="tool">- The tool to be set.</param>
    public void SetPlayerTool(GameObject tool) 
    {
        //Checks if the current tool is not null.
        if(playerTool)
        {
            //Destroys the current tool physical object.
            Destroy(playerTool.gameObject);
        }
        //TO DO: enforce tool parameter argument for function.
        //Checks if the new tool has the PlayerTool component. 
        if(tool.GetComponent<PlayerTool>() != null)
        {
            //Sets the tool's layer to default.
            tool.layer = 0;
            //Sets current tool to equal new tool.
            playerTool = tool.GetComponent<PlayerTool>();
            
           //Sets the tool to become a child of the player.
            tool.transform.SetParent(gameObject.transform, false);
            //Turns the animator off.
            playerTool.DisableAnimator();
            //Sets the tool to the correct position.
            playerTool.transform.localPosition = playerTool.GetToolPosition();
            //Sets the tool to the correct rotation.
            tool.transform.localRotation = Quaternion.Euler(playerTool.GetToolRotation());
            //Sets the tool to the correct scale.
            tool.transform.localScale = playerTool.GetToolScale();
            //Sets the animation flag to true.
            //animatorStatus = true;
        }

    }
}