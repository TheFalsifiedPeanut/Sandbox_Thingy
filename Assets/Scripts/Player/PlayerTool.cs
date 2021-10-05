using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Axe = 0,
    Pickaxe = 1,
    Shovel = 2,
    Flask = 3,
    Gloves = 4,
    Shears = 5
}
[System.Serializable]
public struct ToolID
{
    [SerializeField]
    int ID;
    [SerializeField]
    ToolType toolType;
    public ToolID(int ID, ToolType toolType)
    {
        this.ID = ID;
        this.toolType = toolType;
    }

    public int GetID()
    {
        return ID;
    }
    public ToolType GetToolType()
    {
        return toolType;
    }
}


public class PlayerTool : Item
{
    // The amount of damage the tool will apply to the Harvestable.
    [SerializeField] int toolDamage;
    // The type of tool this is.
    [SerializeField] HarvestingTool harvestingTool;
    // The harvest level this tool is.
    [SerializeField] HarvestingLevel harvestingLevel;
    // The layer to check for when this tool collides with an object on Interactable layer.
    [SerializeField] LayerMask layer;
    [SerializeField] float chopHeight;
    private bool interacting;
    [SerializeField] ToolID toolID;
    [SerializeField] Vector3 handPosition;
    [SerializeField] Vector3 handRotation;
    [SerializeField] Vector3 handScale;
    
    public ToolID GetToolID()
    {
        return toolID;
    }
    public Vector3 GetHandPosition()
    {
        return handPosition;
    }public Vector3 GetHandRotation()
    {
        return handRotation;
    }public Vector3 GetHandScale()
    {
        return handScale;
    }

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
                toolCollider.enabled = false;
            }
        }
    }

    /// <summary>
    /// Set the collider of the tool on.
    /// </summary>
    public void Interact(GameObject interactTarget)
    {

        if (interacting == false)
        {
            Debug.Log("Register");
            Harvestable harvestable = interactTarget.GetComponent<Harvestable>() != null ? interactTarget.GetComponent<Harvestable>() : interactTarget.transform.parent.GetComponent<Harvestable>();

            // Check that that the Harvestable component is not null, this determines if the Interactable is a Harvestable or not..
            if (harvestable != null)
            {


                // Set the animation to be true.
                Debug.Log(chopHeight + transform.position.y);
                Debug.Log(harvestable.transform.position.y < chopHeight + transform.position.y);
                if (harvestable.transform.position.y < chopHeight + transform.position.y - 1)
                {

                    interactAnimation.SetBool("DownChop", true);
                }
                else
                {
                    interactAnimation.SetBool("AcrossChop", true);
                }

                // Set the collider to be enabled for the tool.
                interacting = true;
            }
        }
    }

    /// <summary>
    /// End the interaction.
    /// </summary>
    public void StopInteract()
    {
        // Set the animation to be false.
        interactAnimation.SetBool("AcrossChop", false);
        interactAnimation.SetBool("DownChop", false);
        // Set the collider to be disabled for the tool.
        toolCollider.enabled = false;
    }

    public void EndInteracting()
    {
        Debug.Log("EndInteracting");
        interacting = false;
    }
    public void TurnOnCollider()
    {
        toolCollider.enabled = true;
    }
}