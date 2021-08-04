using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject UISlot;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int slotWidth;
    [SerializeField] int slotHeight;
    [SerializeField] GameObject inventoryParent;
    bool[] slotState;
    GameObject[] UISlots;
    [SerializeField] GameObject UIItem;
    GameObject[] UIItems;

    public void GenerateUI()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 Slotposition = new Vector2((((i * slotWidth) + (Screen.width / 2)) - (slotWidth * width) / 2) + (width % 2 == 1? 0:(slotWidth / 2) ), ((((-j * slotHeight) + (Screen.height / 2)) + (slotHeight * height) / 2) + (height % 2 == 1 ? 0 : (slotHeight / 2))));
                UISlots[Convert2DTo1D(new Vector2Int(i,j))] = (Instantiate(UISlot, Slotposition, Quaternion.identity, inventoryParent.transform));
            }
        }
    }
    private void Start()
    {
        slotState = new bool[width * height];
        UISlots = new GameObject[width * height];
        UIItems = new GameObject[width * height];
        GenerateUI();
    }

    public void AddItem(Texture2D texture) 
    {
        for (int i = 0; i < slotState.Length; i++)
        {
            if(slotState[i] == false)
            {
                slotState[i] = true;
                Vector2 SlotPosition = Convert1DTo2D(i);
                 GameObject NewItem =  Instantiate(UIItem, UISlots[i].transform);
                NewItem.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one);
                UIItems[i] = NewItem;
                return;
            }
        }
    }
    private Vector2Int Convert1DTo2D(int i)
    {
        int x = i / height;
        int y = i % height;
        return new Vector2Int(x,y);
    }
    private int Convert2DTo1D(Vector2Int position)
    {
        return position.x * height + position.y;
    }
}