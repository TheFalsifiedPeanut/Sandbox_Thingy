using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSection : LogHarvest
{
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
        base.OnHarvest();
        Destroy(gameObject);
    }

}
