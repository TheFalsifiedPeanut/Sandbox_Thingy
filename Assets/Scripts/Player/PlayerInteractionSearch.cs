using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionSearch : MonoBehaviour
{
    // The layer to check for when the interaction search occurs.
    [SerializeField] LayerMask layer;
    // A reference to the Player Interact script.
    [SerializeField] PlayerInteract playerInteract;

    // The current shortest distance. Any time there is a distance shorter it is replaced by that distance and the Interaction Object is updated.
    float currentDistance;
    // All the current objects in the interaction area.
    List<GameObject> currentInteractions;

    void Start()
    {
        currentInteractions = new List<GameObject>();
    }

    void Update()
    {
        UpdateClosestInteractable();
    }

    /// <summary>
    /// Update the current closest Interactable.
    /// </summary>
    void UpdateClosestInteractable()
    {
        // Set the Current Distance to -1;
        currentDistance = -1;

        // Loop through all the objects stored in the Current Intereactions list.
        for (int i = 0; i < currentInteractions.Count; i++)
        {
            // Check if the current object is not null.
            if (currentInteractions[i] != null)
            {
                // Cache the distance of the current object. We use the square magnitude rather than a distance check since it performs much faster and fills the needs of comparing distance.
                // Normally when calculating distances the square root is needed, which can be an expensive math operation. Since we don't need to know the length, only which distance is shorter square magnitude is faster.
                float checkDistance = Vector3.SqrMagnitude(transform.position - currentInteractions[i].transform.position);

                // Check if the Current Distance is longer than the distance of the object that we are checking against.
                if (currentDistance > checkDistance || currentDistance == -1)
                {
                    // Set this object as the new Interaction Target.
                    playerInteract.SetInteractionTarget(currentInteractions[i]);
                    // Set the new Current Distance to be the Check Distance so the rest of the objects in the loop check against the new Interaction Target.
                    currentDistance = checkDistance;
                }
            }

            // Remove the null object from the current interactions list.
            else
            {
                currentInteractions.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Check the collision of the interactable area to find any potential Interactable.
    /// </summary>
    /// <param name="other"> The collider the interaction area hit. </param>
    void OnTriggerEnter(Collider other)
    {
        // Check the object the interaction area collided with is on the Interactable layer.
        if ((1 << other.gameObject.layer) == layer.value)
        {
            // Check if the list of Current Interactions contains the collided object.
            if (!currentInteractions.Contains(other.gameObject))
            {
                // Add the collided object to the list of Current Interactions.
                currentInteractions.Add(other.gameObject);
            }
        }
    }

    /// <summary>
    /// Check the collider leaving the area is in the Current Interactables list.
    /// </summary>
    /// <param name="other"> The collider leaving the interaction area. </param>
    void OnTriggerExit(Collider other)
    {
        // Check the object leaving the interaction area is on the Interactable layer.
        if ((1 << other.gameObject.layer) == layer.value)
        {
            // Check if the list of Current Interactions contains the leaving object.
            if (currentInteractions.Contains(other.gameObject))
            {
                // Remove the leaving object from list of Current Interactions.
                currentInteractions.Remove(other.gameObject);
            }

            // Check if the leaving object is the Interaction Target.
            if (playerInteract.GetInteractionTarget() == other.gameObject)
            {
                // Update the Interaction Target.
                UpdateClosestInteractable();
            }
        }
    }
}