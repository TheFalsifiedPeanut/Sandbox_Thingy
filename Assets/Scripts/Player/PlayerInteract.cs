using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

public class PlayerInteract : MonoBehaviour
{
    public PlayerTools playerTools;
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
        playerTools.SubscribeToOnGather(OnGather);
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
    }

    public void StopInteract()
    {
        TargetBox.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == layer.value)
        {
            InteractTarget = other.gameObject;
        }
    }

  

}
