using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InteractionHandler : MonoBehaviour
    {
        private PlayerInputs inputs;
        private InputAction Interact;

        private void Awake()
        {
            inputs = new PlayerInputs();
        }
        private void OnEnable()
        {
            Interact = inputs.Player.Interact;
            Interact.Enable();
        }

        private void OnDisable()
        {
            Interact.Disable();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<IInteractable>() != null)
            {
                if(Interact.IsPressed() )
                    collision.GetComponent<IInteractable>().Interact();
            }
        }
    }

}

