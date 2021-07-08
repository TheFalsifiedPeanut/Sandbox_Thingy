using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHarvest : Harvestable
{
    [SerializeField] protected float harvestSpawnRadius;

    protected override void SpawnHarvest(GameObject harvest)
    {
        Vector3 spawnPoint = transform.position + Random.insideUnitSphere * harvestSpawnRadius;
        spawnPoint.y = 1;
        Instantiate(harvest, spawnPoint, Quaternion.identity);
    }
}
