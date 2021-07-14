using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    // The amount of damage this tool does.
    [SerializeField] protected int toolDamage;
    // The cooldown of this tool.
    [SerializeField] protected int toolCooldown;
    // The type of tool this one is.
    [SerializeField] protected HarvestingTool harvestingTool;
    // The level this tool can harvest.
    [SerializeField] protected HarvestingLevel harvestingLevel;
    // The layer for checking collisions against.
    [SerializeField] protected LayerMask layer;

    private void OnTriggerEnter(Collider other)
    {
        // If the layer is correct.
        if ((1 << other.gameObject.layer) == layer.value)
        {
            // Find the Harvestible component. If it's not on the collided object, check it's parent.
            Harvestable harvestable = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();

            if (harvestable != null)
            {
                harvestable.RemoveHealth(toolDamage, harvestingTool, harvestingLevel);
                TurnOff();
            }
        }
    }

    /// <summary>
    /// Turn off the tool.
    /// </summary>
    public void TurnOff()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    /// <summary>
    /// The cooldown for the tool.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(toolCooldown);

        gameObject.GetComponent<Collider>().enabled = true;
    }
}