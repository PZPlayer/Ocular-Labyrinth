using UnityEngine;
using UnityEngine.InputSystem;

namespace OculusionLabyrinth.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Footprints))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D rb;
        private Footprints footprint;
        private Animator anmtr;
        private Vector2 direction;

        private void Start ()
        {
            rb = GetComponent<Rigidbody2D>();
            anmtr = GetComponent<Animator>();
            footprint = GetComponent<Footprints>();
        }

        private void FixedUpdate ()
        {
            Move();
        }

        private void Move()
        {
            if (direction == Vector2.zero) 
            {
                rb.linearVelocity = Vector2.zero;
                footprint.IsWalking = false;
                anmtr.SetBool("Run", false); 
                return;
            }

            footprint.IsWalking = true;
            transform.rotation = Quaternion.LookRotation(transform.forward, direction);
            anmtr.SetBool("Run", true);
            rb.linearVelocity = direction.normalized * _speed;
        }

        public void OnMove(InputValue value)
        {
            direction = value.Get<Vector2>();
        }
    }
}
