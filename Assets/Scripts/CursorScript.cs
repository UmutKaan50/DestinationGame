//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace y01cu {
    public class CursorScript : MonoBehaviour {

        public Texture2D cursorTexture; // Drag and drop your cursor texture here in the inspector
           public Vector2 hotSpot = Vector2.zero; // Adjust this if needed
           public CursorMode cursorMode = CursorMode.Auto;
       
           void Start()
           {
               SetCustomCursor();
           }
       
           void SetCustomCursor()
           {
               Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
           }
        
        


    }
}