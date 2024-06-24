using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InteractionHandler : MonoBehaviour
    {
        IInteractable interactable;
        bool canInteract;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.started && canInteract)
            {
                interactable.Interact();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<IInteractable>() != null)
            {
                canInteract = true;
                interactable = collision.GetComponent<IInteractable>();
            }else canInteract = false;
        }
    }

}

