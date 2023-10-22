using UnityEngine;

namespace FirstPerson
{
    public class FP_Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] private float acceleration = 1.0f;

        [SerializeField] private float gravity = 12.0f;

        [SerializeField] private float verticalForce = 0.0f;
        [SerializeField] private bool grounded = false;

        [SerializeField] private float jumpForce = 1.0f;
        private bool jump = false;
        private bool prevJump = false;

        private Vector3 moveDir = Vector3.zero;
        private Vector3 targetMoveDir = Vector3.zero;

        private Rigidbody rbody;

        void Start()
        {
            rbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            targetMoveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            jump = Input.GetAxis("Jump") > 0;
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            grounded = false;

            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.0f))
            {
                if (hit.transform.tag == "Ground")
                {
                    grounded = true;
                }
            }

            if (grounded)
            {
                moveDir = Vector3.MoveTowards(moveDir, targetMoveDir, acceleration);

                verticalForce = 0.0f;
                if (jump && !prevJump)
                {
                    verticalForce = jumpForce;
                }
            }
            else
            {
                verticalForce -= gravity;
            }

            Vector3 moveVelocity = moveDir.magnitude > 1.0f ? moveDir.normalized * moveSpeed : moveDir * moveSpeed;
            rbody.velocity = moveVelocity;
            rbody.velocity += new Vector3(0, verticalForce);

            prevJump = jump;
        }
    }
}

