using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//構造体(struct)クラス
public class Struct {

    //ベースのフィールドデータ
    public struct FieldBase {
        public string name;
        public int width;
        public int height;
        public int[,] cells;
    }

    // フィールドのデータ
    public struct Field {
        public string name;
        public int width;
        public int height;
        public CellInfo[,] cells;
    }

    // セル情報
    public struct CellInfo {
        public string name; // 名前
        public int category; // 種類
        public int moveCost; // 移動コスト
        public int avoidanceBonus; // 回避ボーナス
        public int defenseBonus; // 防御ボーナス
        public int magicalDefenseBonus; // 魔防御ボーナス

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="category">Category.</param>
        /// <param name="moveCost">Move cost.</param>
        /// <param name="avoidanceBonus">Avoidance bonus.</param>
        /// <param name="defenseBonus">Defense bonus.</param>
        /// <param name="magicalDefenseBonus">Magical defense bonus.</param>
        public CellInfo(string name, int category, int moveCost, int avoidanceBonus, int defenseBonus, int magicalDefenseBonus) {
            this.name = name;
            this.category = category;
            this.moveCost = moveCost;
            this.avoidanceBonus = avoidanceBonus;
            this.defenseBonus = defenseBonus;
            this.magicalDefenseBonus = magicalDefenseBonus;
        }
    }
    public struct NodeMove {
        public int cost;
        public Enum.AREA aREA;
    }

    public struct NodeRoot {
        public Enum.MOVE move;
        public int cost;

        public NodeRoot(Enum.MOVE move, int cost) {
            this.move = move;
            this.cost = cost;
        }
    }

}
