using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;

public class PlayerPickup : MonoBehaviour
{
    private PlayerInput playerInput;
    public LayerMask layer;
    private Inventory inventory;
    private Harvestable targetHarvest;
    private Coroutine BreakCoroutine;
    private bool interacting;
    public Animator InteractAnimation;

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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(layer.value);
        if ((1 <<  other.gameObject.layer) == layer.value)
        {
            Harvestable otherHarvest = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();
            Debug.Log("1");
            if (otherHarvest != null && otherHarvest != targetHarvest) 
            {
                targetHarvest = otherHarvest;
                Debug.Log(targetHarvest);
                if(interacting)
                {
                }
            }
        }
    }

    public void OnInteract()
    {
        interacting = true;
        Debug.Log("Interacting");
        if (targetHarvest != null)
        {
        }
        InteractAnimation.SetBool("Chop", true);   
    }

    public void StopInteract()
    {
        interacting = false;
        targetHarvest = null;
        if (BreakCoroutine != null)
        {
            StopCoroutine(BreakCoroutine);
        }
        InteractAnimation.SetBool("Chop", false);
    }

    /*public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetHarvest)
        {
            StopInteract();
        }
    } */


    /* private void Pickup() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.5f);
        if (Physics.Raycast(ray, out hit, 3, layer.value)) {

         */

    //inventory.FindStartPosition(hit.transform.GetComponent<IInventoryItem>());


    /*private void StopPickup() {
        Debug.Log("Stop");
        targetHarvest = null;
        if (BreakCoroutine != null)
        {
            StopCoroutine(BreakCoroutine);

        }

    }*/
}
