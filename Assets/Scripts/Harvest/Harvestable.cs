using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HarvestingTool 
{
    PICKAXE,
    AXE,
    SHOVEL,
    LIQUIDCONTAINER,
    HANDS,
    HOE

}

public enum HarvestingLevel
{
    STONETOOL =0,
    IRONTOOL =2,
    BRONZETOOL =1
}

public class Harvestable : MonoBehaviour
{
[SerializeField] protected HarvestingTool harvestingTool;
[SerializeField] protected HarvestingLevel harvestingLevel;
[SerializeField] protected float harvestDurability;

public HarvestingTool GetHarvestingTool() {
    return harvestingTool;
}
public HarvestingLevel GetHarvestingLevel() {
    return harvestingLevel;
}
public float GetHarvestingDurability() {
    return harvestDurability;
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnHarvest()
    {

    }

}
