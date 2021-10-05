using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

public class PlayerInteract : MonoBehaviour
{
    // The current tool. Serialised to the inspector so we can see it.
    [SerializeField] PlayerTool playerTool;
    // The Interaction Search for finding all potential interactable objects.
    [SerializeField] PlayerInteractionSearch playerInteractionSearch;

    // The current Interaction Target.
    [SerializeField] GameObject interactionTarget;
    // The Player Input.
    PlayerInput playerInput;
    // The Player Inventory.
    PlayerInventory playerInventory;
    [SerializeField] LayerMask layer;

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
        playerInventory = GetComponent<PlayerInventory>();
        playerInput.SubscribeToInteract(OnInteract);
        //playerInput.SubscribeToStopInteract(OnStopInteract);
    }
    private void Update()
    {
    }

    /// <summary>
    /// When the player is clicking to interact.
    /// </summary>
    public void OnInteract()
    {
        if (interactionTarget != null)
        {
            Debug.Log("OnInteract");
            playerTool.Interact(interactionTarget);
        }
    }

    /// <summary>
    /// When the player ends interacting.
    /// </summary>
    public void OnStopInteract()
    {
        playerTool.StopInteract();
    }

    public void SetPlayerTool(GameObject tool) 
    {
        if(playerTool)
        {
            Destroy(playerTool.gameObject);
        }

        if(tool.GetComponent<PlayerTool>() != null)
        {
            tool.layer = 0;
            playerTool = tool.GetComponent<PlayerTool>();
            
            Debug.Log("pankake");
            //tool.transform.position = gameObject.transform.position + playerTool.GetHandPosition();
            tool.gameObject.transform.position = Vector3.zero;
            tool.transform.localRotation = Quaternion.Euler(playerTool.GetHandRotation());
            tool.transform.localScale = playerTool.GetHandScale();
            //tool.transform.SetParent(gameObject.transform, false);
            
        }
    }
}