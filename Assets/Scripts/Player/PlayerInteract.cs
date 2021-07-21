using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;
using DitzelGames.FastIK;

public class PlayerInteract : MonoBehaviour
{
    private bool interacting;
    public LayerMask layer;
    public Tool tool;
    private PlayerInput playerInput;
    private Inventory inventory;
    public GameObject targetBox;
    public Animator interactAnimation;
    public GameObject interactTarget;
    public InteractSearch interactSearch;
    public float interactHeightThreshold;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventory = GetComponent<Inventory>();
        playerInput.SubscribeToPickup(OnInteract);
        playerInput.SubscribeToStopPickup(StopInteract);
    }

    /// <summary>
    /// When the player is clicking to interact.
    /// </summary>
    public void OnInteract()
    {
        if(interacting == false)
        {
            StartCoroutine(ClickCooldown());
            interacting = true;

            tool.GetComponent<Collider>().enabled = true;
            if (targetBox != null)
            {
                if (targetBox.transform.position.y < interactHeightThreshold)
                {
                    Debug.Log("Lower Chop");
                }
                else
                {
                    interactAnimation.SetBool("Chop", true);
                    Debug.Log("Upper Chop");
                }
            }
        }
        
        
        
    }

    /// <summary>
    /// When the player ends interacting.
    /// </summary>
    public void StopInteract()
    {
        
        
    }

    public IEnumerator ClickCooldown()
    {
        yield return new WaitForSeconds(3);
        interacting = false;
        interactAnimation.SetBool("Chop", false);
        tool.GetComponent<Collider>().enabled = false;
    }

}