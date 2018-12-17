using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 列挙型定数一覧
/// </summary>
public class Enum {

    public enum AREA { NONE, UNIT, MOVE, ATTACK }

    public enum MOVE { NONE, UP, DOWN, LEFT, RIGHT }

    //プレイヤーの行動
    public enum TURN {
        START,
        SELECT,
        FOCUS,
        MOVE,
        BATTLE,
        RESULT,
        END
    };

    // 敵キャラクターの行動
    public enum ENEMY_TURN {
        START,
        SELECT,
        FOCUS,
        MOVE,
        BATTLE,
        RESULT,
        END
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
}
