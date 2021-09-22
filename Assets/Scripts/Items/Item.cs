using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInventoryItem
{
    [SerializeField] int ID;
    [SerializeField] Texture2D texture;
    [SerializeField] bool inInventory;

    public bool InInventory()
    {
        return inInventory;
    }
    public Texture2D GetTexture()
    {
        return texture;
    }
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
