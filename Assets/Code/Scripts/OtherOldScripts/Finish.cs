//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class Finish : MonoBehaviour
    {
        public void FinishGame() {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
