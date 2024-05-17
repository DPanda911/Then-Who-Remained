using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField]
        public float moveSpeed;

        private PlayerInputs input;
        private InputAction move;

        private Rigidbody2D rb;
        private Animator anim;

        private Vector2 moveDir;
        void Awake()
        {
            input = new PlayerInputs();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }


        private void OnEnable()
        {
            move = input.Player.Move;
            move.Enable();
        }

        private void OnDisable()
        {
            move.Disable();
        }


        void Update()
        {
            moveDir = move.ReadValue<Vector2>();

            if(move.ReadValue<Vector2>() != Vector2.zero)
                anim.SetBool("IsMoving", true);
            else
                anim.SetBool("IsMoving", false);

            anim.SetInteger("PlayerHoriNum", Mathf.RoundToInt(move.ReadValue<Vector2>().x));
            anim.SetInteger("PlayerVertNum", Mathf.RoundToInt(move.ReadValue<Vector2>().y));
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDir * moveSpeed;
        }
    }


}
