    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

/// <summary>
/// Handles the Tree.
/// </summary>
public class Tree : RadiusHarvestable
{
    #region Variables
    /// <summary>
    /// Health whilst standing.
    /// </summary>
    [SerializeField] int standingHealth;
    /// <summary>
    /// Set force needed to push over the tree,
    /// </summary>
    [SerializeField] float pushoverForce;
    /// <summary>
    /// Loot tables for the Tree whilst standing.
    /// </summary>
    [SerializeField] LootTables standingLootTables;
    /// <summary>
    /// Gameobject used to push the tree,
    /// </summary>
    [SerializeField] GameObject pushover;
    /// <summary>
    /// A flag used to check if the tree has fallen.
    /// </summary>
    bool fallen;
    #endregion




    /// <summary>
    /// Removes health from the harvestable by a specified amount. Trees have two sets of health one for standing and one for fallen.
    /// </summary>
    /// <param name="amount"> The amount of health to remove. </param>
    /// <param name="harvestingTool"> The type of tool used. </param>
    /// <param name="harvestingLevel"> The level of the tool used. </param>
    public override void RemoveHealth(int amount, HarvestingTool harvestingTool, HarvestingLevel harvestingLevel)
    {
        // Checks if the flag fallen equals true.
        if (fallen)
        {
            //Removes health.
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
                // Removes health.
                standingHealth -= amount;
                //Calls on harvest.
                OnHarvest();
            }
        }
    }
    ///
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
        //Checks if standing health is less then 0 but not fallen.
        if (standingHealth <= 0 && !fallen)
        {
            //Calls OnHarvested
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
        //Checks if flag fallen equals false.
        if (!fallen)
        {
            // Set the tree to be able to fall then push it in a random direction.
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //Calculates the direction for the tree to fall.
            Vector3 treeDirection = Random.insideUnitCircle;
            
            //Adds the rigidbody and adds force to the object making the tree fall.
            pushover.AddComponent<Rigidbody>().AddForce(new Vector3(treeDirection.x, 0, treeDirection.y).normalized * pushoverForce, ForceMode.Impulse);

            // Get the loot spawn instructions for when the tree is standing.
            List<SpawnInstuction> spawnInstuctions = standingLootTables.GetSpawnInstuctions();

            // Loops through spawnInstuction.
            for (int i = 0; i < spawnInstuctions.Count; i++)
            {
                //Looping through the count of instuction.
                for (int j = 0; j < spawnInstuctions[i].GetCount(); j++)
                {
                    //Calls function used for spawning loot.
                    SpawnHarvest(spawnInstuctions[i].GetSpawnObject());
                }
            }

            // Set fallen to true..
            fallen = true;

            return;
        }

        base.OnHarvested();
    }
}