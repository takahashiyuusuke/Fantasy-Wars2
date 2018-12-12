﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {
    /// <summary>
    /// 基本パラメータ
    /// </summary>
    [Header("基本パラメータ")]
    public int id;
    public string name;
    //public Enums.CLASS_TYPE classType;
    public int level;
    public int hp;
    public int movementRange; // 移動範囲
    public int attackRange; // 攻撃範囲
    public int recoveryCount; // 回復回数

    public Enums.MOVE_TYPE moveType; // 移動タイプ
    [HideInInspector]
    public bool isMoving; // 行動済みかどうか
    public Enums.ARMY aRMY; // 勢力

    [Header("成長パラメータ")]
    public int vitality; // 体力
    public int strength; // 筋力
    public int dexterity; // 技量
    public int intelligence; // 魔力
    public int speed; // 速さ
    public int defense; // 防御
    public int mDefense; // 魔防
    public int luck; // 幸運

    [Header("サブパラメータ")]
    public int physique; // 体格（自分の体格未満のUnitを救出できる）
    public int accompanyId; // 同行UnitのId
    //public Enums.STATUS status; // 状態

    [HideInInspector]
    public MoveController moveController; // 移動管理クラス
}
