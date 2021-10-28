using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

/// <summary>
/// A script used for all interactions.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    /// <summary>
    /// The current tool. Serialised to the inspector so we can see it.
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
    /// The Player Input.
    /// </summary>
    PlayerInput playerInput;
    /// <summary>
    /// The Player Inventory.
    /// </summary>
    PlayerInventory playerInventory;
    /// <summary>
    /// Specifies a layer.
    /// </summary>
    [SerializeField] LayerMask layer;
    /// <summary>
    /// Turns the animator on and off
    /// </summary>
    private bool animatorStatus;

    /// <summary>
    /// Get the current Interaction Target.
    /// </summary>
    /// <returns> Returns the Interaction Target. </returns>
    public GameObject GetInteractionTarget() { return interactionTarget; }

    /// <summary>
    /// Set the current Interaction Target to the specified object.
    /// </summary>
    /// <param name="interactionTarget"> The object to set. </param>
    public void SetInteractionTarget(GameObject interactionTarget) { this.interactionTarget = interactionTarget; }
    void Start()
    {
       
        playerInput = GetComponent<PlayerInput>();
        //Fetches PlayerInventory and asignes it to playerInventory.
        playerInventory = GetComponent<PlayerInventory>();
        //- Runs OnInteract
        playerInput.SubscribeToInteract(OnInteract);
        //playerInput.SubscribeToStopInteract(OnStopInteract);
    }
    private void Update()
    {
        //Checks the animator's status for if it is true
        if (animatorStatus)
        {
            //enables the animator.
            playerTool.EnableAnimator();
            //Turns the animation status back to false.
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
            //Debug.Log("OnInteract");
            //Runs the interact Function with the new parameter interaction target.
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
    /// A script used to pickup tools.
    /// </summary>
    /// <param name="tool">- The tool to be picked up.</param>
    public void SetPlayerTool(GameObject tool) 
    {
        //Checks if the item is a tool.
        if(playerTool)
        {
            //Destroys the physical object.
            Destroy(playerTool.gameObject);
        }
        //- Checks if the script PlayerTool is on the tool.
        if(tool.GetComponent<PlayerTool>() != null)
        {
            //Sets the tool's layer to default.
            tool.layer = 0;
            //Sets playerTool to equal PlayerTool.
            playerTool = tool.GetComponent<PlayerTool>();
            
            //Debug.Log("pankake");
            //tool.transform.position = gameObject.transform.position + playerTool.GetHandPosition();
            //- Sets the tool to become a parent of the player.
            tool.transform.SetParent(gameObject.transform, false);
            //Turns the animator off.
            playerTool.DisableAnimator();
            //Fetches the position that the tool will be in.
            playerTool.GetHandPosition();
            //Sets the tool to the correct position.
            playerTool.transform.localPosition = playerTool.GetHandPosition();
            //Sets the tool to the corect rotation.
            tool.transform.localRotation = Quaternion.Euler(playerTool.GetHandRotation());
            //Sets the tool to the correct scale.
            tool.transform.localScale = playerTool.GetHandScale();
            //animatorStatus = true;
        }

    }
}