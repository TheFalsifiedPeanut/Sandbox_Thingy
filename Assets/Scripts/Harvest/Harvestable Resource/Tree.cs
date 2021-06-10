    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class Tree : LogHarvest
{
    
    [SerializeField] private GameObject pushover;
    private bool fallen;
    [SerializeField] private float pushoverForce;
    [SerializeField] private GameObject treeSection;
    [SerializeField] private int treeSectionCount;



    // Update is called once per frame
    void Update()
    {

    }
    public override void OnHarvest()
    {

        if (fallen == false)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 treeDirection = Random.insideUnitCircle;
            pushover.AddComponent<Rigidbody>();

            pushover.GetComponent<Rigidbody>().AddForce(new Vector3(treeDirection.x, 0, treeDirection.y).normalized * pushoverForce, ForceMode.Impulse);

            fallen = true;

            base.OnHarvest();
           
        } else {
            List<SpawnInstuction> spawnInstuctions = lootTables.spawnInstuctions();
            for (int i = 0; i < spawnInstuctions.Count; i++)
            {
                for (int j = 0; j < spawnInstuctions[i].GetCount(); j++)
                {
                    int RandomDirection = Random.Range(0, 360);
                    Vector2 RandomPosition = Random.insideUnitCircle;
                    Vector3 treeFallPoint = transform.position + new Vector3(RandomPosition.x, 0, RandomPosition.y);
                    Instantiate(spawnInstuctions[i].GetSpawnObject(), treeFallPoint, Quaternion.Euler(0, RandomDirection, 90));
                }
                
                
            }
        }
        Harvested();
    }

    protected override void Harvested()
    {
        if (fallen)
        {
            base.Harvested();
        }

    }



}






