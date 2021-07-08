using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] protected int toolDamage;
    [SerializeField] protected HarvestingTool harvestingTool;
    [SerializeField] protected HarvestingLevel harvestingLevel;
    [SerializeField] protected LayerMask layer;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == layer.value)
        {
            Harvestable harvestable = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();

            if (harvestable != null)
            {
                harvestable.RemoveHealth(toolDamage, harvestingTool, harvestingLevel);
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
    public void TurnOff()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Collider>().enabled = true;
    }
}