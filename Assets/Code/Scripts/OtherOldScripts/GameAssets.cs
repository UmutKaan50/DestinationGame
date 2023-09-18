//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;

namespace Code.Scripts {
    public class GameAssets : MonoBehaviour {

        // Since this is a very specific tiny class it's considered to make a worthwhile trade-off to just call it "i".
        // In this way it can be accesed buy just doing "GameAssets.i" instead of "GameAssets.Instance".

        private static GameAssets _i;

        /// <summary>
        /// Instance of the GameAssets class. It is used to access the assets in the Resources folder.
        /// </summary>
        public static GameAssets i {
            get {
                if (_i == null) 
                    _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            
                return _i;
            }

        }

        public Transform pfDamagePopup;
    }
}
