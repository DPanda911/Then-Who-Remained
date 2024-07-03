using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField]
        public float moveSpeed;

        private Rigidbody2D rb;
        private Animator anim;


        private Vector2 moveDir;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
           moveDir = context.ReadValue<Vector2>();
        }

        void Update()
        {
            if(moveDir != Vector2.zero)
                anim.SetBool("IsMoving", true);
            else
                anim.SetBool("IsMoving", false);

            anim.SetInteger("PlayerHoriNum", Mathf.RoundToInt(moveDir.x));
            anim.SetInteger("PlayerVertNum", Mathf.RoundToInt(moveDir.y));
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDir * moveSpeed;
        }

        public void StopMovement()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("IsMoving", false);
        }
    }


}
