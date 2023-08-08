//
// copyright (c) y01cu. All rights reserved.
//

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace y01cu
{
    /// <summary>
    /// A class that manages player movement alongside their animations.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rigidbody2D;

        private float horizontalMove;
        private float verticalMove;
        [SerializeField] private float speed = 5f;
        private bool isFacingRight = true;

        private Animator animator;
        private AudioSource audioSource;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            // rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);
            rigidbody2D.velocity = new Vector2(horizontalMove * speed, verticalMove * speed);
            if (horizontalMove > 0 && isFacingRight)
                return;
            else if (horizontalMove < 0 && isFacingRight)
                Flip();
            else if (horizontalMove > 0 && !isFacingRight)
                Flip();
            else if (horizontalMove < 0 && !isFacingRight)
                return;

            if (horizontalMove == 0 && verticalMove == 0)
            {
                animator.SetBool("isRunning", false);
            }

            bool isPlayerStopped = verticalMove == 0 && horizontalMove == 0;
            if (isPlayerStopped)
            {
                // isPlayerMoving = false;
                audioSource.Stop();
            }
            else
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        public void Move(InputAction.CallbackContext context)
        {
            animator.SetBool("isRunning", true);
            horizontalMove = context.ReadValue<Vector2>().x;
            verticalMove = context.ReadValue<Vector2>().y;
        }
        
    }
}