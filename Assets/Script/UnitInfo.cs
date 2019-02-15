using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {
    /// <summary>
    /// 基本パラメータ
    /// </summary>
    [Header("基本パラメータ")]
    public int id;
    public string unitName;
    public Enums.CLASS_TYPE classType;　// クラスタイプ
    public int level;
    public int hp;
    public int hpMax; // HPの最大値
    public int movementRange; // 移動範囲
    public int attackRange; // 攻撃範囲
    public int recoveryCount; // 回復回数
    public int exp;

    public Enums.MOVE_TYPE moveType; // 移動タイプ
    [HideInInspector]
    public bool moving = false; // 行動済みかどうか
    public Enums.ARMY aRMY; // 勢力

    [Header("成長パラメータ")]
    public int vitality; // 体力
    public int strengtht; // 筋力
    public int technical; // 技量
    public int intelligence; // 魔力
    public int speed; // 速さ
    public int defense; // 防御
    public int resist; // 魔防
    public int luck; // 幸運

    [Header("サブパラメータ")]
    public int physique; // 体格（自分の体格未満のUnitを救出できる）
    public int accompanyId; // 同行UnitのId
    //public Enums.STATUS status; // 状態

    public string className;


    void Start() {
        hpMax = vitality;
        hp = hpMax;
    }
    /// <summary>
    /// 移動済みかどうかでマテリアルを切り替える
    /// </summary>
    /// <param name="moving">If set to <c>true</c> moving.</param>
    public void Moving(bool moving) {
        this.moving = moving;
    }

    /// <summary>
    /// 外部取得用
    /// </summary>
    /// <returns><c>true</c>, if moving was ised, <c>false</c> otherwise.</returns>
    public bool isMoving() {
        return moving;
    }

    /// <summary>
    /// クラス別のステータス値を返す
    /// </summary>
    /// <returns>The unit class data.</returns>
    /// <param name="classType">Class type.</param>
    public static Struct.UnitClassData GetUnitClassData(Enums.CLASS_TYPE classType) {
        switch (classType)
        {
            case Enums.CLASS_TYPE.SWORDSMAN:
                return SWORDSMAN_MAX_STATUS;
            case Enums.CLASS_TYPE.WIZARD:
                return WIZARD_MAX_STATUS;
            case Enums.CLASS_TYPE.SISTER:
                return ARCHER_MAX_STATUS;
            default:
                return SWORDSMAN_MAX_STATUS;
        }
    }
    // 4 平均　9
    private static readonly Struct.UnitClassData SWORDSMAN_MAX_STATUS = new Struct.UnitClassData()
    {
        className = "剣士", // クラス名
        vitality = 36, // 体力
        attack = 26, // 攻撃
        technical = 26, // 技
        speed = 25, // 速さ
        defense = 26, // 防御
        resist = 24, // 魔防
        luck = 22, // 幸運
        move = 5 // 移動
    };

    private static readonly Struct.UnitClassData WIZARD_MAX_STATUS = new Struct.UnitClassData()
    {
        className = "魔導士", // クラス名
        vitality = 36, // 体力
        attack = 26, // 攻撃
        technical = 26, // 技
        speed = 25, // 速さ
        defense = 26, // 防御
        resist = 24, // 魔防
        luck = 22, // 幸運
        move = 5 // 移動
    };

    private static readonly Struct.UnitClassData ARCHER_MAX_STATUS = new Struct.UnitClassData()
    {
        className = "盗賊", // クラス名
        vitality = 36, // 体力
        attack = 25, // 攻撃
        technical = 26, // 技
        speed = 25, // 速さ
        defense = 26, // 防御
        resist = 24, // 魔防
        luck = 22, // 幸運
        move = 5 // 移動
    };
}
