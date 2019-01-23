using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 計算処理
/// </summary>
public class CommonCalc {

    // 攻撃力の計算
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

}
