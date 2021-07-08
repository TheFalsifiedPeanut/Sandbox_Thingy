using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusHarvestable : Harvestable
{
    [SerializeField] protected float harvestSpawnRadius;

    /// <summary>
    /// Spawns the current harvest from the table.
    /// </summary>
    /// <param name="harvest"> The harvest to spawn. </param>
    protected override void SpawnHarvest(GameObject harvest)
    {
        Vector3 spawnPoint = transform.position + Random.insideUnitSphere * harvestSpawnRadius;
        spawnPoint.y = 1;
        Instantiate(harvest, spawnPoint, Quaternion.identity);
    }
}