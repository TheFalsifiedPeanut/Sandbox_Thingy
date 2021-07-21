using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

public class PlayerInteract : MonoBehaviour
{
<<<<<<< Updated upstream
    private bool interacting;
    public LayerMask layer;
    public Tool tool;
    private PlayerInput playerInput;
    private Inventory inventory;
    public GameObject targetBox;
    public Animator interactAnimation;
    public GameObject interactTarget;
    public InteractSearch interactSearch;
    public float interactHeightThreshold;
=======
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
>>>>>>> Stashed changes

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
<<<<<<< Updated upstream
        inventory = GetComponent<Inventory>();
        playerInput.SubscribeToPickup(OnInteract);
        playerInput.SubscribeToStopPickup(StopInteract);
=======
        playerInventory = GetComponent<PlayerInventory>();
        playerInput.SubscribeToInteract(OnInteract);
        //playerInput.SubscribeToStopInteract(OnStopInteract);
>>>>>>> Stashed changes
    }

    /// <summary>
    /// When the player is clicking to interact.
    /// </summary>
    public void OnInteract()
    {
<<<<<<< Updated upstream
        if(interacting == false)
        {
            StartCoroutine(ClickCooldown());
            interacting = true;

            tool.GetComponent<Collider>().enabled = true;
            if (targetBox != null)
            {
                if (targetBox.transform.position.y < interactHeightThreshold)
                {
                    Debug.Log("Lower Chop");
                }
                else
                {
                    interactAnimation.SetBool("Chop", true);
                    Debug.Log("Upper Chop");
                }
            }
        }
        
        
        
=======
        if (interactionTarget != null)
        {
            playerTool.Interact();
        }
>>>>>>> Stashed changes
    }

    /// <summary>
    /// When the player ends interacting.
    /// </summary>
    public void OnStopInteract()
    {
<<<<<<< Updated upstream
        
        
    }

    public IEnumerator ClickCooldown()
    {
        yield return new WaitForSeconds(3);
        interacting = false;
        interactAnimation.SetBool("Chop", false);
        tool.GetComponent<Collider>().enabled = false;
    }

=======
        playerTool.StopInteract();
    }
>>>>>>> Stashed changes
}