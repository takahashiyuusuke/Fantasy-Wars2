using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// mapManagerの代わり
public class Map : MonoBehaviour {
    static Struct.Field field;

    // 参照渡しで受け取ったフィールデータを更新
    public void LoadData(int mapId) {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        switch (mapId)
        {
            case 1:
                MapData1 mapData1 = gameObject.AddComponent<MapData1>();
                fieldBase = mapData1.GetData();
                Debug.Log("マップデータ"　+ mapId + "が読み込まれました");
                break;
            default:
                Debug.Log("マップデータが読み込まれてません");
                break;
        }

        // フィールドデータの読み込み
        field = new Struct.Field();
        field.name = fieldBase.name;
        field.width = fieldBase.width;
        field.height = fieldBase.height;
        field.cells = new Struct.CellInfo[field.height, field.width];

        // 各セルデータの追加
        for (int y = 0; y < field.height; y++)
        {
            for (int x = 0; x < field.width; x++)
            {
                switch (fieldBase.cells[y, x])
                {
                    // name, category, moveCost, defenseBonus, avoidanceBonus, recoveryBonus; 
                    case 0:
                    default:
                        field.cells[y, x] = new Struct.CellInfo("平地", 0, 1, 0, 0, 0);
                        break;
                    case 1:
                        field.cells[y, x] = new Struct.CellInfo("草むら", 0, 2, 10, 0, 0);
                        break;
                    case 2:
                        field.cells[y, x] = new Struct.CellInfo("壁", 1, 1, 5, 1, 0);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 外部からの呼び出し用
    /// </summary>
    /// <returns>The field data.</returns>
    public static Struct.Field GetFieldData() {
        return field;
    }
}
