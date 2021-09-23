using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolBarUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject[] UISlots;
    [SerializeField] GameObject UIItem;
    [SerializeField] ToolID[] toolIDs;
    UIItemElement[] UIItemElements;
    Dictionary<ToolType, UIItemElement> UISlotState;

    void Awake()
    {
        UIItemElements = new UIItemElement[UISlots.Length];
        UISlotState = new Dictionary<ToolType, UIItemElement>();
        UISlotState.Add(ToolType.Pickaxe, new UIItemElement());
        UISlotState.Add(ToolType.Shovel, new UIItemElement());
        UISlotState.Add(ToolType.Shears, new UIItemElement());
        UISlotState.Add(ToolType.Gloves, new UIItemElement());
        UISlotState.Add(ToolType.Flask, new UIItemElement());
        UISlotState.Add(ToolType.Axe, new UIItemElement());
    }
    public void AddItem(int ID, Texture2D texture)
    {
        for (int j = 0; j < toolIDs.Length; j++)
        {
            if (ID == toolIDs[j].GetID())
            {
                GameObject NewItem = Instantiate(UIItem, UISlots[(int)toolIDs[j].GetToolType()].transform);
                Destroy(UISlotState[toolIDs[j].GetToolType()].GetItem());
                NewItem.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one);
                UISlotState[toolIDs[j].GetToolType()] = new UIItemElement(true, ID, NewItem);
            }
        }
    }
}
