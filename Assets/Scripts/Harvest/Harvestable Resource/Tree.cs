    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class Tree : LogHarvest
{

    [SerializeField] private LootTables lootTablesT2;
    [SerializeField] private GameObject pushover;
    private bool fallen = false;
    [SerializeField] private float pushoverForce;
    [SerializeField] private GameObject treeSection;
    [SerializeField] private int treeSectionCount;
    [SerializeField] private int standingHealth;
    [SerializeField] GameObject[] HitPoints;



    public override void RemoveHealth(int amount)
    {
        OnHarvest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Start()
    {
        base.Start();
        lootTablesT2.initialize();
    }

    public override void OnHarvest()
    {
        standingHealth--;
        if (fallen == false && standingHealth <= 0)
        {
            

            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 treeDirection = Random.insideUnitCircle;
            pushover.AddComponent<Rigidbody>();
            pushover.GetComponent<Rigidbody>().AddForce(new Vector3(treeDirection.x, 0, treeDirection.y).normalized * pushoverForce, ForceMode.Impulse);


            fallen = true;
        }
        else if(fallen)
        {
            if(health <= 0)
            {
                SecondHarvest();
            }
            Debug.LogFormat("Harvested");
            health--;
        }
    }

    protected override void Harvested()
    {
        if (fallen)
        {
            base.Harvested();
        }

    }

    public void SecondHarvest()
    {
        Debug.Log("SecondHarvest");
        List<SpawnInstuction> spawnInstuctions = lootTablesT2.spawnInstuctions();
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
        Harvested();
    }
}






