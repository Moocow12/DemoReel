using UnityEngine;

namespace FirstPerson
{
    public class FP_Player : MonoBehaviour
    {
        private bool canMove = true;
        private bool canLook = true;

        FP_Movement movement;
        FP_Look look;

        private void Start()
        {
            movement = GetComponent<FP_Movement>();
            look = GetComponent<FP_Look>();
        }
        private void FixedUpdate()
        {
            movement.enabled = canMove;
            look.enabled = canLook;
        }
    }
}

