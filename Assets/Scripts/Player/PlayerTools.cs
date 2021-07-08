using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTools : MonoBehaviour
{
    [SerializeField] int toolDamage;
    [SerializeField] HarvestingTool harvestingTool;
    [SerializeField] HarvestingLevel harvestingLevel;
    [SerializeField] LayerMask layer;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == layer.value)
        {
            Harvestable harvestable = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();

            if (harvestable != null)
            {
                harvestable.RemoveHealth(toolDamage, harvestingTool, harvestingLevel);
            }
        }
    }
}
