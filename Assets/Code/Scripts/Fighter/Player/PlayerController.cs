//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//


using Code.Scripts;

namespace Destination.Player {
    using Destination.Enemies;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Old class that was used for player movement.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : Mover {
        private SpriteRenderer spriteRenderer;
        private bool isAlive = true;
        public bool hasKey = false;
        private bool isPlayerMoving;
        private bool isVerticalMoving;
        private bool isHorizontalMoving;

        private float horizontalMove;
        private float verticalMove;

        public AudioSource audioSource;
        public Portal portal;

        [SerializeField] private float rayCastDistance;

        private void FixedUpdate() {
            ManagePlayerMovement();
            ManagePlayerHit();
        }

        private void ManagePlayerHit() {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, rayCastDistance);

            if (hit.collider != null) {
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
            else {
                Debug.DrawLine(transform.position, transform.position + transform.right * rayCastDistance, Color.green);
            }
        }

        public void ManagePlayerMovement() {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            float verticalMove = Input.GetAxisRaw("Vertical");

            if (isAlive)
                UpdateMotor(new Vector3(horizontalMove, verticalMove, 0));

            bool isPlayerStopped = verticalMove == 0 && horizontalMove == 0;
            if (isPlayerStopped) {
                isPlayerMoving = false;
                audioSource.Stop();
            }
            else {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }

            // With joystick:
            //if(joystick.Horizontal >= .2f) {
            //    horizontalMove = xSpeed;

            //} else if (joystick.Horizontal <= -.2f) {
            //    horizontalMove = -xSpeed;
            //} else {
            //    horizontalMove = 0f;
            //    isHorizontalMoving = false;
            //}

            //if (joystick.Vertical >= .2f) {
            //    verticalMove = ySpeed;
            //} else if (joystick.Vertical <= -.2f) {
            //    verticalMove = -ySpeed;
            //} else {
            //    verticalMove = 0f;
            //    isVerticalMoving = false;
            //}
        }

        protected override void Start() {
            base.Start();
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
            // DontDestroyOnLoad(gameObject);
        }

        public void SetHasKey() {
            hasKey = true;
            portal.SetCanTransfer();
        }

        protected override void RecieveDamage(Damage dmg) {
            if (!isAlive)
                return;

            base.RecieveDamage(dmg);
            GameManager.instance.OnHitpointChange();
        }

        protected override void Death() {
            isAlive = false;
            GameManager.instance.deathMenuAnim.SetTrigger("Show");
        }

        public void SwapSprite(int skinId) {
            spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
        }

        public void OnLevelUp() {
            maxHitpoint++;
            hitpoint = maxHitpoint;
        }

        public void SetLevel(int level) {
            for (int i = 0; i < level; i++)
                OnLevelUp();
        }

        public void Heal(int healingAmount) {
            if (hitpoint == maxHitpoint)
                return;

            hitpoint += healingAmount;

            if (hitpoint > maxHitpoint)
                hitpoint = maxHitpoint;
            GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position,
                Vector3.up * 30, 1.0f);
            GameManager.instance.OnHitpointChange();
        }

        public void Respawn() {
            Heal(maxHitpoint);
            isAlive = true;
            lastImmune = Time.time;
            pushDirection = Vector3.zero;
        }

        [SerializeField] private Rigidbody2D rigidBody2D;

        // -- NEW INPUT SYSTEM --

        private Vector3 rawInputMovement;
        private float horizontalMovement;
        private float verticalMovement;

        private void Move(InputAction.CallbackContext context) {
            horizontalMovement = context.ReadValue<Vector2>().x;
            verticalMovement = context.ReadValue<Vector2>().y;
        }
    }
}