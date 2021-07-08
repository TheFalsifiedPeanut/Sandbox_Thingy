using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTools : MonoBehaviour
{

    private Action<Harvestable> OnGather;
    public LayerMask layer;
    private bool OnCooldown;
    public float CooldownTimer;

    public void SubscribeToOnGather(Action<Harvestable> OnGather)
    {
        this.OnGather = OnGather;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(OnCooldown == false)
        {
            Debug.Log(layer.value);
            if ((1 << other.gameObject.layer) == layer.value)
            {
                Harvestable otherHarvest = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();
                Debug.Log("1");
                if (otherHarvest != null)
                {
                    Debug.Log("healthy trees");
                    OnGather(otherHarvest);
                    OnCooldown = true;
                }
            }
        }
        
    }

    public void Cooldown()
    {
        
        OnCooldown = false;
    }

    public void TurnOff()
    {
        OnCooldown = true;
    }





}
