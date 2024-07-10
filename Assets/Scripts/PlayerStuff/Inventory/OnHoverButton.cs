using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Should do a thing here?");
        GetComponent<Button>().onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Should do a thing here?");
        GetComponent<Button>().onClick.Invoke();
    }
}
    

