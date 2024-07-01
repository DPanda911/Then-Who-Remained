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
}
