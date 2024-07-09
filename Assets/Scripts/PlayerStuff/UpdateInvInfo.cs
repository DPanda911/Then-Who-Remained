using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateInvInfo : MonoBehaviour
{
    [SerializeField] bool isBlankScreen;

    public GenericItem item;
    [SerializeField] private GameObject DisplayScreen;

    public void UpdateItemInfo(GenericItem newItem)
    {
        item = newItem;
        DisplayScreen.SetActive(true);
        DisplayScreen.GetComponentInChildren<TMP_Text>().text = item.description;
        DisplayScreen.GetComponentInChildren<Image>().sprite = item.Icon;
        //DisplayScreen.GetComponentInChildren<Button>(). figure out how to easily call different functions depending on what item is selected
    }

    public void UpdateItemInfo() => DisplayScreen.SetActive(false); //if no item is assigned no item display
    
}
