using UnityEngine;

public class GameAssets : MonoBehaviour {
    // Since this is a very specific tiny class it's considered to make a worthwhile trade-off to just call it "i".
    // In this way it can be accesed buy just doing "GameAssets.i" instead of "GameAssets.Instance".

    private static GameAssets _i;

    public Transform pfDamagePopup;

    public static GameAssets i {
        get {
            if (_i == null)
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));

            return _i;
        }
    }
}