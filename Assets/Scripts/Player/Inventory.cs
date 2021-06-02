using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeorgeProject;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryUI;
    private int [,] inventoryGrid;
    private PlayerInput playerInput;
    private Dictionary<int, IInventoryItem> InventoryItemReference;
    public GameObject InventoryItemPrefab;
    public List<GameObject> GridUI; 


    private void Start() {
        inventoryGrid = new int [6,4];
        InventoryItemReference = new Dictionary<int, IInventoryItem>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.SubscribeToToggleUI(ToggleUI);
        for(int x = 0; x < inventoryGrid.GetLength(0); x++) {
            for(int y = 0; y < inventoryGrid.GetLength(1); y++) {
                inventoryGrid[x,y ] = -1;
            }
            
        }
        //test code
        
    }
    private void ToggleUI() {
        if(InventoryUI.activeSelf) {
            InventoryUI.SetActive(false);
        }   else{
            InventoryUI.SetActive(true);
        }
    }
        public void FindStartPosition(IInventoryItem item) {
            for(int x = 0; x < inventoryGrid.GetLength(0); x++) {
                for(int y = 0; y < inventoryGrid.GetLength(1); y++) {
                    if(Add(item, x, y)) {
                        return;
                    }
                }
            }
        }

        public bool Add(IInventoryItem item, int xStart, int yStart) { 
        if(xStart >= 0 && yStart >= 0 && xStart < inventoryGrid.GetLength(0) && yStart < inventoryGrid.GetLength(1)) {

        
        for(int x = 0; x < item.getShapeX(); x++) {
            for(int y = 0; y < item.getShapeY(); y++) {
                if(xStart + x >= inventoryGrid.GetLength(0) || yStart + y >= inventoryGrid.GetLength(1) ) {    
                    return false;
                } 
                if(inventoryGrid[xStart + x, yStart + y] != -1) {
                    return false;
                }
            }
        }
        int ID = FindFreeID();
        InventoryItemReference.Add(ID, item);
        for(int x = 0; x < item.getShapeX(); x++) {
            for(int y = 0; y < item.getShapeY(); y++) {
                inventoryGrid[xStart + x, yStart + y] = ID;
            }
        }
        GameObject itemUI = Instantiate(InventoryItemPrefab, GridUI[Index2D(xStart,yStart)].transform.position + new Vector3(-50,50,0), Quaternion.identity, InventoryUI.transform);
        
        itemUI.GetComponent<RectTransform>().sizeDelta = (new Vector3(item.getShapeX(),item.getShapeY(),1) * 100);
        
        item.SetUiElement(itemUI);
        return true;
        }
        return false;
        }
    public void RemoveItem(int xStart, int yStart) {
        if(xStart >= 0 && xStart < inventoryGrid.GetLength(0) && yStart >= 0 && yStart < inventoryGrid.GetLength(1) ) {
            if(inventoryGrid[xStart, yStart] != -1) {
                int removeID = inventoryGrid[xStart, yStart];
                Destroy(InventoryItemReference[removeID].getUIElement());
                InventoryItemReference.Remove(removeID);
                for(int x = 0; x < inventoryGrid.GetLength(0); x++) {
                    for(int y = 0; y < inventoryGrid.GetLength(1); y++) {
                        if(inventoryGrid[x, y] == removeID)  {
                            inventoryGrid[x, y] = -1;
                        } 
                    }
                }
            }
        }
    }
    private int Index2D(int x, int y) {
        return y * 6 + x;
    } 
    private int FindFreeID() {
        for(int i = 0; i < Mathf.Infinity; i++) {
            if(!InventoryItemReference.ContainsKey(i)) {
                return i;
            }
        } 
        return -1;
    }
}

    
