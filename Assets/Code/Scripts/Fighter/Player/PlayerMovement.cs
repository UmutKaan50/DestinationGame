//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

namespace Destination.Player {
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// A class that manages player movement alongside their animations.
    /// </summary>
    public class PlayerMovement : MonoBehaviour {
        public Rigidbody2D rigidbody2D;

        // private float horizontalMove;
        // private float verticalMove;
        [SerializeField] private float speed = 5f;
        private bool isFacingRight = true;

        private Animator animator;
        private AudioSource audioSource;

        private PlayerInputActions playerInputActions;

        // private Vector2 movementInputVector;
    
        
        private void Awake() {
            // movementInputVector = new Vector2(0, 0);
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            playerInputActions = GetComponent<PlayerInputSystem>().GetPlayerInputActions();
        }

        private void Flip() {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        public void UpdatePlayerMovement(Vector2 movementInputVector) {
            float horizontalMove = movementInputVector.x;
            float verticalMove = movementInputVector.y;

            // rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);

            bool isPlayerStopped = verticalMove == 0 && horizontalMove == 0;
            if (isPlayerStopped) {
                animator.SetBool("isRunning", false);
                // isPlayerMoving = false;
                audioSource.Stop();
            }
            else {
                animator.SetBool("isRunning", true);
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }

            if (horizontalMove > 0 && isFacingRight)
                return;
            else if (horizontalMove < 0 && isFacingRight)
                Flip();
            else if (horizontalMove > 0 && !isFacingRight)
                Flip();
            else if (horizontalMove < 0 && !isFacingRight)
                return;
        }
    }
}