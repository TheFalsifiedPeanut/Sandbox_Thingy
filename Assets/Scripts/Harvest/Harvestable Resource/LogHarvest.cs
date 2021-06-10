using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHarvest : Harvestable
{
    [SerializeField] protected float radiusFromObject;



    // Update is called once per frame
    void Update()
    {

    }


    public override void OnHarvest()
    {
        List<SpawnInstuction> spawnInstuctions = lootTables.spawnInstuctions();
        for (int i = 0; i < spawnInstuctions.Count; i++)
        {
            for (int j = 0; j < spawnInstuctions[i].GetCount(); j++)
            {
                Vector3 spawnPoint = transform.position + Random.insideUnitSphere * radiusFromObject;
                spawnPoint.y = 1;
                if (droppedHarvestable != null)
                {
                    Instantiate(spawnInstuctions[i].GetSpawnObject(), spawnPoint, Quaternion.identity);
                }
            }

        }
    }


}
