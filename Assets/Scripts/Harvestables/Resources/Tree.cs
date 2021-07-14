    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class Tree : RadiusHarvestable
{

    [SerializeField]int standingHealth;
    [SerializeField]float pushoverForce;
    [SerializeField]LootTables standingLootTables;
    [SerializeField]GameObject pushover;
    
    bool fallen;

    /// <summary>
    /// Removes health from the harvestable by a specified amount. Trees have two sets of health one for standing and one for fallen.
    /// </summary>
    /// <param name="amount"> The amount of health to remove. </param>
    /// <param name="harvestingTool"> The type of tool used. </param>
    /// <param name="harvestingLevel"> The level of the tool used. </param>
    public override void RemoveHealth(int amount, HarvestingTool harvestingTool, HarvestingLevel harvestingLevel)
    {
        // If the tree has fallen use the base RemoveHealth.
        if (fallen)
        {
            base.RemoveHealth(amount, harvestingTool, harvestingLevel);

            return;
        }

        // If the tree is standing remove health from it's standingHealth;
        // Check if the correct tool is being used. It's designed so some tools can be used in multiple ways and that some resources can be harvest by different tools.
        if (harvestingTool.HasFlag(harvestingTool))
        {
            // Check if the tool level used is high enough.
            if (this.harvestingLevel <= harvestingLevel)
            {
                // Take health and call the OnHarvest.
                standingHealth -= amount;
                OnHarvest();
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        // Initialise the secondary loot tables.
        standingLootTables.Initialise();
    }

    /// <summary>
    /// This is called every time the harvestable is harvested. Trees have two sets of health one for standing and one for fallen.
    /// </summary>
    public override void OnHarvest()
    {
        if (standingHealth <= 0 && !fallen)
        {
            OnHarvested();

            return;
        }

        base.OnHarvest();
    }

    /// <summary>
    /// This is called when the harvestable has no health left. Trees have two sets of health one for standing and one for fallen.
    /// </summary>
    protected override void OnHarvested()
    {
        if (!fallen)
        {
            // Set the tree to be able to fall then push it in a random direction.
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 treeDirection = Random.insideUnitCircle;
            pushover.AddComponent<Rigidbody>();
            pushover.GetComponent<Rigidbody>().AddForce(new Vector3(treeDirection.x, 0, treeDirection.y).normalized * pushoverForce, ForceMode.Impulse);

            // Get the loot spawn instructions for when the tree is standing.
            List<SpawnInstuction> spawnInstuctions = standingLootTables.GetSpawnInstuctions();

            // Follow the spawn instructions.
            for (int i = 0; i < spawnInstuctions.Count; i++)
            {
                for (int j = 0; j < spawnInstuctions[i].GetCount(); j++)
                {
                    SpawnHarvest(spawnInstuctions[i].GetSpawnObject());
                }
            }

            // Set the tree to fallen.
            fallen = true;

            return;
        }

        base.OnHarvested();
    }

    /// <summary>
    /// Spawns the current harvest from the table.
    /// </summary>
    /// <param name="harvest"> The harvest to spawn. </param>
    protected override void SpawnHarvest(GameObject harvest)
    {
        // Get a random rotation for logs and spawn them.
        int randomDirection = Random.Range(0, 360);
        Vector3 spawnPoint = transform.position + Random.insideUnitSphere * harvestSpawnRadius;
        spawnPoint.y = 1;
        Instantiate(harvest, spawnPoint, Quaternion.Euler(0, randomDirection, 90));
    }
}