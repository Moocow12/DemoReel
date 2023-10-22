using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPerson
{
    public class FP_Door : MonoBehaviour
    {
        [SerializeField] private float range = 1.0f;

        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            float distanceToPlayer = (FP_GameManager.instance.player.transform.position - transform.position).magnitude;
            animator.SetBool("character_nearby", distanceToPlayer <= range);
        }
    }
}
