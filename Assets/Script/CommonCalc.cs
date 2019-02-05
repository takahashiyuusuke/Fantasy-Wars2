﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 計算処理
/// </summary>
public class CommonCalc {

    // 攻撃する側の攻撃力計算
    public int GetAttackPoint(UnitInfo myUnit) {
        // 攻撃力＝筋力
        return myUnit.strengtht;
    }

    public int GetAttackDamage(UnitInfo myUnit, UnitInfo targetUnit) {
        // 攻撃力-防御力＋地形ボーナス
        int damage = GetAttackPoint(myUnit)
            - targetUnit.defense
            + Main.GameManager.GetMap().field.cells[
                -(int)targetUnit.transform.position.y,
                (int)targetUnit.transform.position.x].defenseBonus;
        return (0 < damage) ? damage : 0;
    }

    /// <summary>
    /// 攻撃の命中率を返す
    /// </summary>
    /// <returns>The hit rate.</returns>
    /// <param name="myUnit">My unit.</param>
    /// <param name="targetUnit">Target unit.</param>
    public int GetHitRate(UnitInfo myUnit, UnitInfo targetUnit) {
        // 命中 - 回避
        int hitRate = GetHitVal(myUnit) - GetDodgeVal(targetUnit);
        return Mathf.Clamp(hitRate, 0, 100);
    }

    /// <summary>
    /// 攻撃の命中値を返す
    /// </summary>
    /// <returns>The hit value.</returns>
    /// <param name="unitInfo">Unit info.</param>
    public int GetHitVal(UnitInfo unitInfo) {
        // 参考　=(技×3+幸運)/2+(装備武器の命中+非特効補正)+装備武器のレベル補正+3すくみ補正+クラス補正
        // =(技×3+幸運)/2+装備武器のレベル補正
        int hitVal = (unitInfo.technical * 3 + unitInfo.luck) + 1; // 仮設定

        return (0 < hitVal) ? hitVal : 0;
    }

    /// <summary>
    /// 回避値を返す
    /// </summary>
    /// <returns>The dodge value.</returns>
    /// <param name="unitInfo">Unit info.</param>
    public int GetDodgeVal(UnitInfo unitInfo) {
        // 参考 =(速×3+幸運)/2+装備武器の回避+地形効果+クラス補正
        //  =(速×3+幸運)/2 + 地形効果
        int dodgeVal = (unitInfo.speed * 3 + unitInfo.luck) / 2
            + Main.GameManager.GetMap().field.cells[
            -(int)unitInfo.transform.position.y,
            (int)unitInfo.transform.position.x
        ].avoidanceBonus;

        return (0 < dodgeVal) ? dodgeVal : 0;
    }

    /// <summary>
    /// 攻撃回数を返す
    /// </summary>
    /// <param name="myUnit"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public int GetAttackCount(UnitInfo myUnit, UnitInfo targetUnit) {
        // 自分のスピード - 相手のスピード >= 8 なら2回
        return (myUnit.speed - targetUnit.speed >= 5) ? 2 : 1;
    }

    /// <summary>
    /// 必殺率を返す
    /// </summary>
    /// <param name="myUnit"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public int GetDeathBlowRete(UnitInfo myUnit, UnitInfo targetUnit) {
        // 参考 必殺	=(技-4)/2+装備武器の必殺+クラス補正
        // 参考 必殺回避	=幸運/2+装備武器の必殺回避+クラス補正

        // (技 -/ 2 + 武器補正値) - (敵の幸運 / 2)
        int deathBlowRate = (myUnit.technical / 2 + 1) - (targetUnit.luck / 2);

        return Mathf.Clamp(deathBlowRate, 0, 100);
    }

    /// <summary>
    /// 実効命中率による攻撃命中判定
    /// </summary>
    /// <returns><c>true</c>, if hit decision was gotten, <c>false</c> otherwise.</returns>
    /// <param name="hitRate">Hit rate.</param>
    public bool GetHitDecision(int hitRate) {
        // 0 ~ 99の乱数を二つ生成
        int r1 = Random.Range(0, 100);
        int r2 = Random.Range(0, 100);

        // 二つの乱数の平均値を求める(小数点切り捨て)
        int r3 = (int)Mathf.Floor((r1 + r2) / 2);

        // 平均値が命中率以下であれば命中とする
        return (r3 < hitRate) ? true : false;
    }

    /// <summary>
    /// 確率の成功判定
    /// </summary>
    /// <returns><c>true</c>, if check was randomed, <c>false</c> otherwise.</returns>
    /// <param name="probability">Probability.</param>
    public bool ProbabilityDecision(int probability) {
        return UnityEngine.Random.Range(0, 100) < probability ? true : false;
    }

    /// <summary>
    /// posAからposBまでのセル数を返す
    /// </summary>
    /// <param name="posA"></param>
    /// <param name="posB"></param>
    /// <returns></returns>
    public int GetCellDistance(Vector3 posA, Vector3 posB) {
        return Mathf.Abs((int)posA.x - (int)posB.x) + Mathf.Abs((int)posA.y - (int)posB.y);
    }

    /// <summary>
    /// レベル毎の最大経験値
    /// </summary>
    /// <returns>The exp max.</returns>
    /// <param name="level">Level.</param>
    public int GetExpMax(int level) {
        // Lv1 = 1200exp (敵3体)
        // Lv2 = 1400exp
        // Lv10 = 3000exp
        // Lv20 = 5000exp
        // Lv 40(MAX) = 9000exp (敵23体)
        return 1000 + (200 * level);
    }

}
