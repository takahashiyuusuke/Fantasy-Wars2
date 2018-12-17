using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    // ユニット指定用のカーソル座標
    private Vector3 EnemyCursor, _enemyCursor;



    // 敵ユニットの座標
    [HideInInspector]
    public UnitInfo focusEnemy;
    private Vector3 oldFocusEnemyPos;

    // インスタンス
    private RouteManager routeManager;


    // 


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // 配置リスト取得
        Main.GameManager.GetMapUnitData();

        Main.GameManager.GetMapUnit(oldFocusEnemyPos);

    }

    // 敵の移動処理
    public void EnemyMove() {


        // 配置リスト上での敵ユニット情報の移動
        Main.GameManager.MoveMapUnitData(oldFocusEnemyPos, focusEnemy.GetComponent<MoveController>().getPos());
    }
}
