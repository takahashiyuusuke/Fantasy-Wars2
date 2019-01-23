using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 計算処理
/// </summary>
public class CommonCalc {

    // 攻撃する側の攻撃力計算
    public int GetAttackPoint(UnitInfo myUnit) {
        // 攻撃力＝筋力
        return myUnit.strength;
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
        int hitVal = (unitInfo.dexterity * 3 + unitInfo.luck) + 1; // 仮設定

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
}
