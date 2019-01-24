using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour {

    // Use this for initialization
    private void Start() {

    }

    // Update is called once per frame
    private void Update() {

    }
    /// <summary>
    /// グレースケールの切り替え
    /// </summary>
    /// <param name="val">If set to <c>true</c> value.</param>
    public void GrayScale(bool val) {
        if (val)
            GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Material/GrayScale");
        else
        {
#if UNITY_EDITOR
            GetComponent<SpriteRenderer>().material = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat");
#else
            unitObj.GetComponent<SpriteRenderer>().material  = Resources.GetBuiltinResource<Material>("Sprites-Default.mat");
#endif
        }

    }

}
