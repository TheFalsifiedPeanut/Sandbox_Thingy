using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The type of tool used to harvest.
/// </summary>
[Flags] public enum HarvestingTool 
{
    PICKAXE = 1,
    AXE = 2,
    SHOVEL = 4,
    LIQUIDCONTAINER = 8,
    HANDS = 16,
    HOE = 32

}

/// <summary>
/// The level of tools used to harvest.
/// </summary>
public enum HarvestingLevel
{
    STONETOOL = 0,
    BRONZETOOL = 1,
    IRONTOOL = 2
}

public class Harvestable : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected HarvestingTool harvestingTool;
    [SerializeField] protected HarvestingLevel harvestingLevel;
    [SerializeField] protected LootTables lootTables;

    #region Health

    /// <summary>
    /// Get the current health of the harvestable.
    /// </summary>
    /// <returns> Returns the current health. </returns>
    public int GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Set the health of the harvestable.
    /// </summary>
    /// <param name="health"> The health value to set. </param>
    public void SetHealth(int health)
    {
        this.health = health;
    }

    /// <summary>
    /// Removes health from the harvestable by a specified amount.
    /// </summary>
    /// <param name="amount"> The amount of health to remove. </param>
    /// <param name="harvestingTool"> The type of tool used. </param>
    /// <param name="harvestingLevel"> The level of the tool used. </param>
    public virtual void RemoveHealth(int amount, HarvestingTool harvestingTool, HarvestingLevel harvestingLevel)
    {
        Debug.Log("Health Lowered!");
        // Check if the correct tool is being used. It's designed so some tools can be used in multiple ways and that some resources can be harvest by different tools.
        Debug.Log(harvestingTool);
        Debug.Log(this.harvestingTool);
        if (this.harvestingTool.HasFlag(harvestingTool))
        {
            // Check if the tool level used is high enough.
            if (this.harvestingLevel <= harvestingLevel)
            {
                // Take health and call the OnHarvest.
                health -= amount;
                OnHarvest();
            }
        }
    }

    #endregion

    #region Tool

    /// <summary>
    /// Get the harvesting tool required.
    /// </summary>
    /// <returns> Returns the required harvesting tool. </returns>
    public HarvestingTool GetHarvestingTool()
    {
        return harvestingTool;
    }

    /// <summary>
    /// Get the harvesting level required.
    /// </summary>
    /// <returns> Returns the required harvesting level. </returns>
    public HarvestingLevel GetHarvestingLevel()
    {
        return harvestingLevel;
    }

    #endregion

    protected virtual void Start()
    {
        // Initialise the loot tables.
        Debug.Log(gameObject.name);
        lootTables.Initialise(gameObject);
    }

    #region Harvesting

    /// <summary>
    /// This is called every time the harvestable is harvested.
    /// </summary>
    public virtual void OnHarvest()
    {
        if (health <= 0)
        {
            OnHarvested();
        }
    }

    /// <summary>
    /// This is called when the harvestable has no health left.
    /// </summary>
    protected virtual void OnHarvested()
    {
        // Get the loot spawn instructions for the harvestable.
        List<SpawnInstuction> spawnInstuctions = lootTables.GetSpawnInstuctions();

        // Follow the spawn instructions.
        for (int i = 0; i < spawnInstuctions.Count; i++)
        {
            for (int j = 0; j < spawnInstuctions[i].GetCount(); j++)
            {
                SpawnHarvest(spawnInstuctions[i].GetSpawnObject());
            }
        }
        
        Destroy(gameObject);
    }

    /// <summary>
    /// Spawns the current harvest from the table.
    /// </summary>
    /// <param name="harvest"> The harvest to spawn. </param>
    protected virtual void SpawnHarvest(GameObject harvest)
    {
        Instantiate(harvest, transform.position, Quaternion.identity);
    }

    #endregion
}
