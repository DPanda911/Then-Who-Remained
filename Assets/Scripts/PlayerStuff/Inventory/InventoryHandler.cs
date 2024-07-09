using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryHandler : MonoBehaviour
{
    public static InventoryHandler _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public List<GenericItem> CurrentItems = new List<GenericItem>();

    
    [SerializeField] GameObject Inventory;
    [SerializeField] TMP_Text[] buttonNames;

    bool opened;
    /// <TODO>
    /// make a keybind to open the inventory
    /// display a button for every item in the current inv
    /// when clicking on a button related to said item, display an enlared image and the discription from the item
    /// 
    /// Info needed: 
    /// should the inventory be a set size or unlimited?
    /// should items have some sort of use that is done in inventoty?
    /// if limited inventory, what do we do when player is full and needs to pick up another item?
    /// 
    /// </TODO>


    public void AddItemToInv(GenericItem item)
    {
        foreach (GenericItem item2 in CurrentItems)
        {
            if(item2 == item)
            {
                return; //check to ensure they dont already have the item
            }
        }
        CurrentItems.Add(item);
    }

    public void RemoveItemFromInv(GenericItem item)
    {
        if (CurrentItems.Contains(item))
            CurrentItems.Remove(item);
        else
        {
            Debug.Log("Tried removing an item you did not have, \n how has this happened.");
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        //we should also have a check for wether or not the player should be able to open the inventory but 
        //I dont feel like it at the moment
        if (context.started)
        {
            if (!opened)
                OpenInv();
            else
                CloseInv();
        }
    }

    void OpenInv()
    {
        opened = true;
        Inventory.SetActive(true);
        PlayerDisable.Instance.DisablePMovement(true);
        GenInvDisplay();
    }

    void CloseInv()
    {
        Inventory.SetActive(false);
        PlayerDisable.Instance.DisablePMovement(false);
        opened = false;
    }
    void GenInvDisplay()
    {
        int i = 0;
        foreach (TMP_Text text in buttonNames) text.text = "placeholder"; //I know there's a better way to do this, I can not think of it right now
        
        foreach (GenericItem item in CurrentItems)
        {
            buttonNames[i].text = item.itemName;
            buttonNames[i].transform.parent.gameObject.SetActive(true);

            i++;
        }

        foreach(TMP_Text text in buttonNames)
        {
            if(text.text == "placeholder")
            {
                text.transform.parent.gameObject.SetActive(false);
                Debug.Log("found placeholders");
            }
        }
    }

}
