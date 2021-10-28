using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerTool : Item
{
    #region Variables
    /// <summary>
    /// The amount of damage the tool will apply to the Harvestable.
    /// </summary>
    [SerializeField] int toolDamage;
    /// <summary>
    /// The type of tool this is.
    /// </summary>
    [SerializeField] HarvestingTool harvestingTool;
    /// <summary>
    /// The harvest level this tool is.
    /// </summary>
    [SerializeField] HarvestingLevel harvestingLevel;
    /// <summary>
    /// The layer to check for when this tool collides with an object on Interactable layer.
    /// </summary>
    [SerializeField] LayerMask layer;
    /// <summary>
    /// Decides which animation to use at height.
    /// </summary>
    [SerializeField] float actionHeight;
    /// <summary>
    /// A flag for when the tool is interacting.
    /// </summary>
    bool interacting;
    #region Tool Positioning
    /// <summary>
    /// Where a Tool will be positioned when picked up.
    /// </summary>
    [SerializeField] Vector3 toolPosition;
    /// <summary>
    /// How a Tool will be rotated when picked up.
    /// </summary>
    [SerializeField] Vector3 toolRotation;
    /// <summary>
    /// How a Tool will be scaled when picked up.
    /// </summary>
    [SerializeField] Vector3 toolScale;
    #endregion
    #endregion

    #region Getters
    /// <summary>
    /// Returns the type of tool.
    /// </summary>
    /// <returns>HarvestingTool</returns>
    public HarvestingTool GetHarvestingTool()
    {
        return harvestingTool;
    }
    #region ToolGetters
    /// <summary>
    /// Gets where the tool will be positioned.
    /// </summary>
    /// <returns>Tool Position.</returns>
    public Vector3 GetToolPosition()
    {
        return toolPosition;
    }
    /// <summary>
    /// Gets how the tool will be rotated.
    /// </summary>
    /// <returns>Tool Rotation.</returns>
    public Vector3 GetToolRotation()
    {
        return toolRotation;
    }
    /// <summary>
    /// Gets how the tool will be scaled.
    /// </summary>
    /// <returns>Tool Scale.</returns>
    public Vector3 GetToolScale()
    {
        return toolScale;
    }
    #endregion
    #endregion


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
        Debug.Log(1 << other.gameObject.layer);
        Debug.Log(layer.value);
        // Check the object the tool collided with is on the Interactable layer.
        if ((1 << other.gameObject.layer) == layer.value)
        {
            Debug.Log(other.gameObject);
            // Cache the Harvestable componenet.
            Harvestable harvestable = other.transform.GetComponent<Harvestable>() != null ? other.transform.GetComponent<Harvestable>() : other.transform.GetComponentInParent<Harvestable>();
            //TO DO: FIX NULLS.
            // Check that that the Harvestable component is not null, this determines if the Interactable is a Harvestable or not..
            if (harvestable != null)
            {
                Debug.Log("layer");
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
                interactAnimation.enabled = true;
                if (harvestable.transform.position.y < actionHeight + transform.position.y - 1)
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
        interactAnimation.enabled = false;
        // Set the animation to be false.
        interactAnimation.SetBool("AcrossChop", false);
        interactAnimation.SetBool("DownChop", false);
        // Set the collider to be disabled for the tool.
        toolCollider.enabled = false;
    }

    public void EndInteracting()
    {
        interactAnimation.enabled = false;
        Debug.Log("EndInteracting");
        interacting = false;
    }
    public void TurnOnCollider()
    {
        toolCollider.enabled = true;
    }
    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void EnableAnimator()
    {
        GetComponent<Animator>().enabled = true;
    }
}