using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] PlayerInventory playerInventory;
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == layer.value)
        {
            if(other.GetComponent<IInventoryItem>() != null)
            {
                if(other.GetComponent<IInventoryItem>().InInventory() != true)
                {
                    playerInventory.AddItem(other.GetComponent<Item>(), 1);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
