using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public bool PickedUp;

    private void Start()
    {
        PickedUp = false;
    }

    public void ItemPickup() 
    {
        if(gameObject.tag == "PickupItem")
        {

        }
    }



}
