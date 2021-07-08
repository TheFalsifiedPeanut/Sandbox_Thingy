using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

public class PlayerInteract : MonoBehaviour
{
    public Tool tool;
    public Animator InteractAnimation;
    private Inventory inventory;
    private PlayerInput playerInput;
    private bool Interacting;
    public BoxCollider TargetBox;
    public LayerMask layer;
    public GameObject InteractTarget;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventory = GetComponent<Inventory>();
        playerInput.SubscribeToPickup(OnInteract);
        playerInput.SubscribeToStopPickup(StopInteract);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        TargetBox.enabled = true;
        InteractAnimation.SetBool("Chop", true);
        Interacting = true;
        tool.GetComponent<Collider>().enabled = true;
    }

    public void StopInteract()
    {
        TargetBox.enabled = false;
        InteractAnimation.SetBool("Chop", false);
        Interacting = false;
        tool.GetComponent<Collider>().enabled = false;
    }
}
