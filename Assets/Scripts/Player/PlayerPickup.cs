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
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventory = GetComponent<Inventory>();
        playerInput.SubscribeToPickup(Pickup);
        playerInput.SubscribeToStopPickup(StopPickup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Pickup() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.5f);
        if(Physics.Raycast(ray,out hit, 3,layer.value)) {
            if(hit.transform.GetComponent<Harvestable>() != null)
            {
                if (hit.transform.GetComponent<Harvestable>() != targetHarvest)
                {
                    targetHarvest = hit.transform.GetComponent<Harvestable>();
                    Debug.Log(targetHarvest);
                    BreakCoroutine = StartCoroutine(HarvestTimer(targetHarvest, HarvestingLevel.STONETOOL));
                }
            }
            
            //inventory.FindStartPosition(hit.transform.GetComponent<IInventoryItem>());

        }
    }
    private void StopPickup() {
        Debug.Log("Stop");
        targetHarvest = null;
        if(BreakCoroutine != null) 
        {
            StopCoroutine(BreakCoroutine);
            
        }
        
    }
    private IEnumerator HarvestTimer(Harvestable harvestable, HarvestingLevel harvestingLevel) {
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
