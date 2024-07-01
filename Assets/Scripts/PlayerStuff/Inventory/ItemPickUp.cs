using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    [SerializeField] GenericItem itemToPickUp;

    public void Interact()
    {
        InventoryHandler._instance.AddItemToInv(itemToPickUp);

        Destroy(gameObject);
    }
}
