using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct UIItemElement
{
    private bool state;
    int id;
    GameObject item;
    public UIItemElement(bool state, int id, GameObject item)
    {
        this.state = state;
        this.id = id;
        this.item = item;
    }

    public bool GetState()
    {
        return state;
    }
    public int GetID()
    {
        return id;
    }
    public GameObject GetItem()
    {
        return item;
    }

}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject UISlot;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int slotWidth;
    [SerializeField] int slotHeight;
    [SerializeField] GameObject inventoryParent;
    GameObject[] UISlots;
    [SerializeField] GameObject UIItem;
    UIItemElement[] UIItemElements;
    [SerializeField]PlayerInput PlayerInput;

    public void GenerateUI()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 Slotposition = new Vector2((((i * slotWidth) + (Screen.width / 2)) - (slotWidth * width) / 2) + (width % 2 == 1 ? 0 : (slotWidth / 2)), ((((-j * slotHeight) + (Screen.height / 2)) + (slotHeight * height) / 2) + (height % 2 == 1 ? 0 : (slotHeight / 2))));
                UISlots[Convert2DTo1D(new Vector2Int(i, j))] = (Instantiate(UISlot, Slotposition, Quaternion.identity, inventoryParent.transform));
            }
        }
    }
    private void Start()
    {
        UISlots = new GameObject[width * height];
        UIItemElements = new UIItemElement[width * height];
        GenerateUI();
        PlayerInput.SubscribeToToggleUI(ToggleUI);
    }

    private void ToggleUI()
    {
        inventoryParent.SetActive(inventoryParent.activeSelf ? false : true);
    }

    public void AddItem(int ID, Texture2D texture)
    {
        for (int i = 0; i < UIItemElements.Length; i++)
        {
            if (UIItemElements[i].GetState() == false)
            {

                Vector2 SlotPosition = Convert1DTo2D(i);
                GameObject NewItem = Instantiate(UIItem, UISlots[i].transform);
                NewItem.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one);
                UIItemElements[i] = new UIItemElement(true, ID, NewItem);
                return;
            }
        }
    }
    public void RemoveItem(int ID)
    {
        for (int i = 0; i < UIItemElements.Length; i++)
        {
            if (UIItemElements[i].GetID() == ID)
            {
                Destroy(UIItemElements[i].GetItem());
                UIItemElements[i] = new UIItemElement(false, 0, null);
            }
        }
    }

    private Vector2Int Convert1DTo2D(int i)
    {
        int x = i / height;
        int y = i % height;
        return new Vector2Int(x, y);
    }
    private int Convert2DTo1D(Vector2Int position)
    {
        return position.x * height + position.y;
    }
} 