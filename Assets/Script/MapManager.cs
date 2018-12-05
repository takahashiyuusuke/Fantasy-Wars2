using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MapManager : MonoBehaviour {

    static Struct.Field field;

    // 参照渡しで受け取ったフィールデータを更新
    public void LoadData(int mapId) {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        MapDatas mapDatas= new MapDatas();
        switch (mapId)
        {
            case 10:
                fieldBase = mapDatas.GetData10();
                break;

            case 9:

                fieldBase = mapDatas.GetData9();
                break;
            case 8:
                fieldBase = mapDatas.GetData8();
                break;

            case 7:

                fieldBase = mapDatas.GetData7();
                break;
            case 6:
                fieldBase = mapDatas.GetData6();
                break;

            case 5:

                fieldBase = mapDatas.GetData5();
                break;
            case 4:
                fieldBase = mapDatas.GetData4();
                break;

            case 3:

                fieldBase = mapDatas.GetData3();
                break;

            case 2:
                fieldBase = mapDatas.GetData2();
                break;

            case 1:
                
                fieldBase = mapDatas.GetData1();
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
                    // name, category, moveCost, avoidanceBonus, defenseBonus, recoveryBonus; 
                    case 0:
                    default:
                        field.cells[y, x] = new Struct.CellInfo("平地", 0, 1, 0, 0, 0, 0, 0);
                        break;
                    case 1:
                        field.cells[y, x] = new Struct.CellInfo("草むら", 0, 2, 10, 0, 0, 0, 0);
                        break;
                    case 2:
                        field.cells[y, x] = new Struct.CellInfo("壁", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 3:
                        field.cells[y, x] = new Struct.CellInfo("城門", 0, 1, 20, 2, 0, 5, 0);
                        break;
                        //case 3:
                        //	field.cells[y, x] = new Struct.CellInfo("沼", 0, 2, -5, 0, 0);
                        //	break;
                        //case 4:
                        //	field.cells[y, x] = new Struct.CellInfo("熱い床", 0, 2, -15, 0, 0);
                        //	break;
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
