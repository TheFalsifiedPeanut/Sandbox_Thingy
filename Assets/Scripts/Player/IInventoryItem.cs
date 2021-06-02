using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    public string GetItemName();
    public int getShapeX();
    public int getShapeY();
    public int getID();
    public void SetUiElement(GameObject gameObject);

    public GameObject getUIElement();
}
