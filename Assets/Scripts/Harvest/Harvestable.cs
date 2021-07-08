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
    [SerializeField] protected GameObject droppedHarvestable;
    [SerializeField] protected LootTables lootTables;
    [SerializeField] protected int health;

    public HarvestingTool GetHarvestingTool()
    {
        return harvestingTool;
    }
    public HarvestingLevel GetHarvestingLevel()
    {
        return harvestingLevel;
    }
    public float GetHarvestingDurability()
    {
        return harvestDurability;
    }
    public int GetHealth()
    {
        return health;
    }
    public void SetHealth(int health)
    {
        this.health = health;
    }
    public virtual void RemoveHealth(int amount)
    {
        health -= amount;
        OnHarvest();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        lootTables.initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnHarvest()
    {
        Harvested();
    }
    protected virtual void Harvested()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
