using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolBarUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject[] UISlots;
    [SerializeField] GameObject UIItem;
    UIItemElement[] UIItemElements;
    Dictionary<HarvestingTool, UIItemElement> UISlotState;
    Dictionary<int, HarvestingTool> Tools;

    void Awake()
    {
        UIItemElements = new UIItemElement[UISlots.Length];
        UISlotState = new Dictionary<HarvestingTool, UIItemElement>();
        UISlotState.Add(HarvestingTool.PICKAXE, new UIItemElement());
        UISlotState.Add(HarvestingTool.SHOVEL, new UIItemElement());
        UISlotState.Add(HarvestingTool.SHEARS, new UIItemElement());
        UISlotState.Add(HarvestingTool.GLOVES, new UIItemElement());
        UISlotState.Add(HarvestingTool.FLASK, new UIItemElement());
        UISlotState.Add(HarvestingTool.AXE, new UIItemElement());
    }
    public void AddItem(PlayerTool playerTool)
    {
        HarvestingTool harvestingTool = playerTool.GetHarvestingTool();
        GameObject newToolUI = Instantiate(UIItem, UISlots[(int)harvestingTool].transform);
        Destroy(UISlotState[harvestingTool].GetItem());
        Texture2D toolTexture = playerTool.GetTexture();
        newToolUI.GetComponent<Image>().sprite = Sprite.Create(toolTexture, new Rect(0, 0, toolTexture.width, toolTexture.height), Vector2.one);
        UISlotState[harvestingTool] = new UIItemElement(true, playerTool.GetID(), newToolUI);
    }
}
