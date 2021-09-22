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
    [SerializeField] int height;
    [SerializeField] GameObject inventoryParent;
    [SerializeField ]GameObject[] UISlots;
    [SerializeField] GameObject UIItem;
    UIItemElement[] UIItemElements;
    [SerializeField] PlayerInput playerInput;

    private void Awake()
    {
        //UISlots = new GameObject[width * height];
        UIItemElements = new UIItemElement[UISlots.Length];
       //GenerateUI();
        playerInput.SubscribeToToggleUI(ToggleUI);
        ToggleUI();
        Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;

        Cursor.visible = Cursor.visible ? false : true;
    }

    private void ToggleUI()
    {
        playerInput.ToggleLock();
        inventoryParent.SetActive(inventoryParent.activeSelf ? false : true);
        Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;

        Cursor.visible = Cursor.visible ? false : true;
    }

    public void AddItem(int ID, int count, Texture2D texture)
    {
        for (int i = 0; i < UIItemElements.Length; i++)
        {
            if (UIItemElements[i].GetState() == false)
            {

                Vector2 SlotPosition = Convert1DTo2D(i);
                Debug.Log(UISlots[i]);
                GameObject NewItem = Instantiate(UIItem, UISlots[i].transform);
                NewItem.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one);
                NewItem.GetComponentInChildren<Text>().text = count.ToString();
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

    public void ModifyAmount(int amount, int ID)
    {
        for (int i = 0; i < UIItemElements.Length; i++)
        {
            if (UIItemElements[i].GetID() == ID)
            {
                UIItemElements[i].GetItem().GetComponentInChildren<Text>().text = amount.ToString();
            }
        }
    }
}