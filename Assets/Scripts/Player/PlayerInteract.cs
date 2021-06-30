using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;

public class PlayerInteract : MonoBehaviour
{
    public PlayerTools playerTools;
    public Animator InteractAnimation;
    private Inventory inventory;
    private PlayerInput playerInput;
    private bool Interacting;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventory = GetComponent<Inventory>();
        playerInput.SubscribeToPickup(OnInteract);
        playerInput.SubscribeToStopPickup(StopInteract);
        playerTools.SubscribeToOnGather(OnGather);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        InteractAnimation.SetBool("Chop", true);
        Interacting = true;
    }

    public void StopInteract()
    {
        InteractAnimation.SetBool("Chop", false);
        Interacting = false;
    }

    public void OnGather(Harvestable harvestable)
    {
        if(Interacting == true)
        {
            harvestable.RemoveHealth(1);
        }
        
    }

}
