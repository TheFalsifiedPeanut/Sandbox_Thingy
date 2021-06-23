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

            Debug.Log("1");
            if (other.transform.GetComponent<Harvestable>() != null)
            {
                Debug.Log("2");
                if (other.transform.GetComponent<Harvestable>() != targetHarvest)
                {
                    targetHarvest = other.transform.GetComponent<Harvestable>();
                    Debug.Log(targetHarvest);
                }
            }
        }
    }

    public void OnInteract()
    {
        if(targetHarvest != null)
        {
            interacting = true;
            BreakCoroutine = StartCoroutine(HarvestTimer(targetHarvest, HarvestingLevel.STONETOOL));
            
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

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetHarvest)
        {
            StopInteract();
        }
    }


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
    private IEnumerator HarvestTimer(Harvestable harvestable, HarvestingLevel harvestingLevel)
    {
        Debug.Log("Hello");
        Debug.Log("WaitForSeconds" + (int)harvestingLevel + 1);

        yield return new WaitForSeconds(harvestable.GetHarvestingDurability() / ((int)harvestingLevel + 1));
        Debug.Log("bye");
        harvestable.OnHarvest();
        IInventoryItem item = harvestable.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Debug.Log("Pickup");
            inventory.FindStartPosition(item);
        }

    }
}
