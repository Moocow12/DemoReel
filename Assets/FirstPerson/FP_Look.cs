using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPerson
{
    public class FP_Look : MonoBehaviour
    {
        [SerializeField] private float lookSpeed = 1.0f;

        [SerializeField] private Camera playerCamera;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 pitchRotation = new Vector3(-Input.GetAxis("Mouse Y"), 0.0f, 0.0f);
            playerCamera.transform.Rotate(pitchRotation * lookSpeed);

            Vector3 yawRotation = new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f);
            transform.Rotate(yawRotation * lookSpeed);
        }
    }
}

