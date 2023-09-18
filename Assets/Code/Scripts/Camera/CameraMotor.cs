//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class CameraMotor : MonoBehaviour {

        private Transform lookAt;
        public float boundX = 0.15f;
        public float boundY = 0.05f;
        [SerializeField] private string nameOfThePlayerThatCameraWillFollow;
    
        private void Start() {
            lookAt = GameObject.Find(nameOfThePlayerThatCameraWillFollow).transform;
        }

        private void LateUpdate() {

            Vector3 delta = Vector3.zero;
            // This is to check if we're inside the bounds in the X axis.
            float deltaX = lookAt.position.x - transform.position.x;
            if (deltaX > boundX || deltaX < -boundX) {
                if (transform.position.x < lookAt.position.x) {
                    delta.x = deltaX - boundX;
                } else {
                    delta.x = deltaX + boundX;
                }
            }

            float deltaY = lookAt.position.y - transform.position.y;
            if (deltaY > boundY || deltaY < -boundY) {
                if (transform.position.y < lookAt.position.y) {
                    delta.y = deltaY - boundY;
                } else {
                    delta.y = deltaY + boundY;
                }
            }

            transform.position += new Vector3(delta.x, delta.y, 0);
        }
    }
}
