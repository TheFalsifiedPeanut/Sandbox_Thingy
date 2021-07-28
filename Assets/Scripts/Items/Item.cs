using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInventoryItem
{
    [SerializeField] int ID;


    public int GetHeight()
    {
        throw new System.NotImplementedException();
    }

    public int GetID()
    {
        return ID;
    }

    public string GetItemName()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetUIElement()
    {
        throw new System.NotImplementedException();
    }

    public int GetWidth()
    {
        throw new System.NotImplementedException();
    }

    public void SetUIElement(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }
}
