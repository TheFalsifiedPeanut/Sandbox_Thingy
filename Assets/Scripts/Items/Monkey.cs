using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Harvestable,IInventoryItem  {
    public int getID( ) {
        return ID;
    }
    int ID;
    string Name;
    GameObject UiElement;
    public int getShapeX( ) {
        return 3;
    }
    public int getShapeY( ) {
        return 3;
    }
    public string GetItemName() {
        return Name;
    }

    public GameObject getUIElement() {
        return UiElement;
    }


    public void SetUiElement(GameObject gameObject) {
        UiElement = gameObject;
    }
}
