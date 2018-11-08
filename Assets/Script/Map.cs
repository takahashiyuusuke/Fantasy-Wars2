using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    //マップ移動のコスト
    public int cost;

    // 座標保存用
    public static Vector3 pos;


	// Use this for initialization
	void Start () {
        GetComponent<UnitManager>();
	}

    //クリックされたマスの座標を取得
    public void GetPos() {
        pos = transform.position;
        Debug.Log(pos);
    }
    public void aaaaaaa() {

    }

}
