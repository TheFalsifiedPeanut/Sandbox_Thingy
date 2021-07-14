using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSearch : MonoBehaviour
{
    public LayerMask layer;
    public PlayerInteract playerInteract;
    float CurrentDistance;
    List<GameObject> CurrentInteractions;
    // Start is called before the first frame update
    void Start()
    {
        CurrentDistance = -1;
        CurrentInteractions = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer) == layer.value)
        {
            if(CurrentDistance == -1 || CurrentDistance > Vector3.SqrMagnitude(this.transform.position - other.gameObject.transform.position))
            {
                playerInteract.targetBox = other.gameObject;
            } else
            {
                CurrentInteractions.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < CurrentInteractions.Count; i++)
        {
            if(CurrentDistance > Vector3.SqrMagnitude(this.transform.position - CurrentInteractions[i].transform.position))
            {
                playerInteract.targetBox = CurrentInteractions[i];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer) == layer.value)
        {
            if (other.gameObject == playerInteract)
            {
                playerInteract.targetBox = CurrentInteractions.Count > 0 ? CurrentInteractions[0] : null;
                for (int i = 0; i < CurrentInteractions.Count; i++)
                {
                    if (CurrentDistance > Vector3.SqrMagnitude(this.transform.position - CurrentInteractions[i].transform.position))
                    {
                        playerInteract.targetBox = CurrentInteractions[i];
                    }
                }
            } else
            {
                if(CurrentInteractions.Contains(other.gameObject))
                {
                    CurrentInteractions.Remove(other.gameObject);
                }
            }
        }
    }

    public void SetPlayerInteract(PlayerInteract playerInteract)
    {
        //Debug.Log(playerInteract);
        //this.playerInteract = playerInteract;
    }




}
