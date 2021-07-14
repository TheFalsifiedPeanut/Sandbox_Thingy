using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

public class PlayerInteract : MonoBehaviour
{
    private bool interacting;
    public LayerMask layer;
    public Tool tool;
    private PlayerInput playerInput;
    private Inventory inventory;
    public GameObject targetBox;
    public Animator interactAnimation;
    public GameObject interactTarget;
    public InteractSearch interactSearch;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventory = GetComponent<Inventory>();
        playerInput.SubscribeToPickup(OnInteract);
        playerInput.SubscribeToStopPickup(StopInteract);
        interactSearch.SetPlayerInteract(this);
    }

    /// <summary>
    /// When the player is clicking to interact.
    /// </summary>
    public void OnInteract()
    {
        interacting = true;
        interactAnimation.SetBool("Chop", true);
        tool.GetComponent<Collider>().enabled = true;
    }

    /// <summary>
    /// When the player ends interacting.
    /// </summary>
    public void StopInteract()
    {
        interacting = false;
        interactAnimation.SetBool("Chop", false);
        tool.GetComponent<Collider>().enabled = false;
    }
    
}