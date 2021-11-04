using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class that modifies drops.
/// </summary>
public class RadiusHarvestable : Harvestable
{
    /// <summary>
    /// Radius where dropped items could spawn.
    /// </summary>
    [SerializeField] protected float harvestSpawnRadius;

    /// <summary>
    /// Spawns the current harvest from the table.
    /// </summary>
    /// <param name="harvest"> The harvest to spawn. </param>
    protected override void SpawnHarvest(GameObject harvest)
    {
        // Gets a random rotation for items and spawn them.
        int randomDirection = Random.Range(0, 360);
        //Finds a position for the items to spawn in the spawn radius.
        Vector3 spawnPoint = transform.position + Random.insideUnitSphere * harvestSpawnRadius;
        //Sets height of spawned objects.
        spawnPoint.y = 5;
        //Creates new loot.
        Instantiate(harvest, spawnPoint, Quaternion.Euler(0, randomDirection, 90));
    }
}