using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMineralRocks : Harvestable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] protected int minLogs;
    [SerializeField] protected int maxLogs;
    [SerializeField] protected float radiusFromObject;

    // Start is called before the first frame update


    public override void OnHarvest()
    {
        int logCount = Random.Range(minLogs, maxLogs);
        for (int i = 0; i < logCount; i++)
        {
            Vector3 spawnPoint = transform.position + Random.insideUnitSphere * radiusFromObject;
            spawnPoint.y = 1;
            if (droppedHarvestable != null)
            {
                Instantiate(droppedHarvestable, spawnPoint, Quaternion.identity);
            }
        }
    }


}
