using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] PlayerInventory playerInventory;
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == layer.value)
        {
            if(other.GetComponent<IInventoryItem>() != null)
            {
                playerInventory.AddItem(other.GetComponent<IInventoryItem>(), 1);
                Destroy(other.gameObject);
            }
        }
    }
}
