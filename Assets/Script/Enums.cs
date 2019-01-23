using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 列挙型定数一覧
/// </summary>
public class Enums {

    public enum AREA { NONE, UNIT, MOVE, ATTACK }

    public enum MOVE { NONE, UP, DOWN, LEFT, RIGHT }


    /// <summary>
    /// プレイヤーの行動
    /// </summary>
    public enum PHASE {
        START, // プレイヤーのターン開始時
        SELECT, // Unit選択中
        FOCUS, // Unit選択時
        MOVE, // Unit行動時
        BATTLE_STANDBY, // Unit攻撃選択時
        BATTLE, // Unit攻撃時
        RESULT, // Unit攻撃終了時（まだ見操作のUnitがいれば、SELECTに戻る）
        END // プレイヤーのターン終了時
    }

    //勢力
    public enum ARMY {
        ALLY, // プレイヤー側（青Unit）
        ENEMY, // 敵（赤Unit）
        NEUTRAL
    }

    //キャラの移動タイプ
    public enum MOVE_TYPE {
        WALKING, // 
        ATHLETE, // 
        HORSE,   //
        FLYING,  //
    }

    public enum BATTLE {
        NORMAL,
        DEATH_BLOW,
        MISS
    }
}
