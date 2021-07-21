using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : MonoBehaviour
{
    // The amount of damage the tool will apply to the Harvestable.
    [SerializeField] int toolDamage;
    // The type of tool this is.
    [SerializeField] HarvestingTool harvestingTool;
    // The harvest level this tool is.
    [SerializeField] HarvestingLevel harvestingLevel;
    // The layer to check for when this tool collides with an object on Interactable layer.
    [SerializeField] LayerMask layer;

    Collider toolCollider;
    Animator interactAnimation;

    void Start()
    {
        toolCollider = GetComponent<Collider>();
        interactAnimation = GetComponent<Animator>();
    }

    /// <summary>
    /// Get the Interaction Animation of the tool.
    /// </summary>
    /// <returns> Returns the Interaction Animation. </returns>
    public Animator GetInteractionAnimation() { return interactAnimation; }

    /// <summary>
    /// Check the collision of the tool to see if a Harvestable was hit.
    /// </summary>
    /// <param name="other"> The collider the tool hit. </param>
    void OnTriggerEnter(Collider other)
    {
        // Check the object the tool collided with is on the Interactable layer.
        if ((1 << other.gameObject.layer) == layer.value)
        {
            // Cache the Harvestable componenet.
            Harvestable harvestable = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();

            // Check that that the Harvestable component is not null, this determines if the Interactable is a Harvestable or not..
            if (harvestable != null)
            {
                // Apply damage to the Harvestable.
                harvestable.RemoveHealth(toolDamage, harvestingTool, harvestingLevel);
                // Stop the interaction.
                StopInteract();
            }
        }
    }

    /// <summary>
    /// Set the collider of the tool on.
    /// </summary>
    public void Interact()
    {
        // Set the animation to be true.
        interactAnimation.SetBool("Chop", true);
        // Set the collider to be enabled for the tool.
        toolCollider.enabled = true;
    }

    /// <summary>
    /// End the interaction.
    /// </summary>
    public void StopInteract()
    {
        // Set the animation to be false.
        interactAnimation.SetBool("Chop", false);
        // Set the collider to be disabled for the tool.
        toolCollider.enabled = false;
    }
}