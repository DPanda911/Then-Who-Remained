using UnityEngine;
using UnityEngine.Events;

public class ItemUse : MonoBehaviour, IInteractable
{
    [SerializeField] GenericItem item;

    [SerializeField] UnityEvent ItemTriggerEvent;

    public void Interact()
    {
        InventoryHandler._instance.RemoveItemFromInv(item);
        ItemTriggerEvent.Invoke();

        Destroy(gameObject);
    }
}
