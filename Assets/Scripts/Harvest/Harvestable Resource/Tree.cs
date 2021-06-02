    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class Tree : Harvestable
{
    [SerializeField]
    private GameObject DroppedHarvestable;
    [SerializeField] private int minLogs;
    [SerializeField] private int maxLogs;
    [SerializeField] private float radiusFromTree;
    [SerializeField] private GameObject pushover;
    private bool fallen;
    [SerializeField] private float pushoverForce;
    [SerializeField] private GameObject treeSection;
    [SerializeField] private int treeSectionCount;


    // Start is called before the first frame update
    void Start()
    {

    }

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


            int logCount = Random.Range(minLogs, maxLogs);
            for (int i = 0; i < logCount; i++)
            {
                Vector3 spawnPoint = transform.position + Random.insideUnitSphere * radiusFromTree;
                spawnPoint.y = 1;
                if (DroppedHarvestable != null)
                {
                    Instantiate(DroppedHarvestable, spawnPoint, Quaternion.identity);
                }
            }
        } else {
            for (int i = 0; i < treeSectionCount; i++)
            {
                int RandomDirection = Random.Range(0, 360);
                Vector2 RandomPosition = Random.insideUnitCircle;
                Vector3 treeFallPoint = transform.position + new Vector3(RandomPosition.x, 0, RandomPosition.y);
                Instantiate(treeSection, treeFallPoint, Quaternion.Euler(0, RandomDirection, 90));
                
            }
            Destroy(gameObject);
        }
    }
    

}






