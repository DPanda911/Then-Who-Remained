using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] GameObject[] invHolder;
    [SerializeField] GameObject Inventory;
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

    void DisplayInventory()
    {

    }

}
