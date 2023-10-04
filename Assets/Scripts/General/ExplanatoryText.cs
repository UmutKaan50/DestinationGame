//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

#if UNITY_EDITOR
using UnityEngine;

namespace y01cu {
    public class ExplanatoryText : MonoBehaviour {
        [TextArea(5, 10)] public string message;
    }
}
#endif