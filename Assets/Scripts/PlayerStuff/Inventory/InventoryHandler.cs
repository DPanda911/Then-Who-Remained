using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [SerializeField] private GameObject DisplayScreen;
    GenericItem _item;

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

        if (buttonNames[0].isActiveAndEnabled)
            buttonNames[0].GetComponentInParent<Button>().Select();
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
            Debug.Log(item.itemName);
            buttonNames[i].text = item.itemName;
            buttonNames[i].transform.parent.gameObject.SetActive(true);
            buttonNames[i].GetComponentInParent<Button>().onClick.RemoveAllListeners();
            buttonNames[i].GetComponentInParent<Button>().onClick.AddListener(
                ()=> { UpdateItemInfo(item); TESTFORBUTTONIFWORKSAHH(); }
                );
            //buttonNames[i].GetComponentInParent<EventTrigger>().OnSelect()
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

    void UpdateItemInfo(GenericItem newItem)
    {
        _item = newItem;
        DisplayScreen.SetActive(true);
        DisplayScreen.GetComponentInChildren<TMP_Text>().text = _item.description;
        DisplayScreen.GetComponentInChildren<Image>().sprite = _item.Icon;
        //DisplayScreen.GetComponentInChildren<Button>(). figure out how to easily call different functions depending on what item is selected
    }

    void UpdateItemInfo() => DisplayScreen.SetActive(false); //if no item is assigned no item display

    void TESTFORBUTTONIFWORKSAHH()
    {
        Debug.Log("I took so long trying to figure out why this did not work, only to realize I never set the Item display in the inspector ); ");
    }

}
