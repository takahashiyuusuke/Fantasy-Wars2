using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MapManager {

    public Struct.Field field;

    public MapManager(int mapId) {
        // マップデータの取得
        GetMapData(mapId);
        Debug.Log(mapId);
    }

    // 参照渡しで受け取ったフィールデータを更新
    public void GetMapData(int mapId) {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        MapDatas mapDatas = new MapDatas();
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
                    // name, category, moveCost, avoidanceBonus, defenseBonus, magicalDefenseBonus, hpBonus,      hpOnus; 
                    // 名前, 種類,     コスト,   回避率,         防御ボーナス, 魔防ボーナス,        回復値,       地形ダメージ
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
                    case 4:
                        field.cells[y, x] = new Struct.CellInfo("沼", 0, 2, -5, 0, 0, 0, 0);
                        break;
                    case 5:
                        field.cells[y, x] = new Struct.CellInfo("熱い床", 0, 2, -15, 0, 0, 0, 5);
                        break;
                    case 6:
                        field.cells[y, x] = new Struct.CellInfo("川", 1, 2, -15, 0, 0, 0, 5);
                        break;
                    case 7:
                        field.cells[y, x] = new Struct.CellInfo("水晶", 1, 2, -15, 0, 0, 0, 5);
                        break;
                    case 8:
                        field.cells[y, x] = new Struct.CellInfo("森", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 9:
                        field.cells[y, x] = new Struct.CellInfo("村", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 10:
                        field.cells[y, x] = new Struct.CellInfo("城", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 11:
                        field.cells[y, x] = new Struct.CellInfo("木", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 12:
                        field.cells[y, x] = new Struct.CellInfo("山", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 13:
                        field.cells[y, x] = new Struct.CellInfo("切り株", 0, 2, 10, 0, 0, 0, 0);
                        break;
                    case 14:
                        field.cells[y, x] = new Struct.CellInfo("岩", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 15:
                        field.cells[y, x] = new Struct.CellInfo("穴", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 16:
                        field.cells[y, x] = new Struct.CellInfo("看板", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 17:
                        field.cells[y, x] = new Struct.CellInfo("池", 1, 2, -15, 0, 0, 0, 5);
                        break;
                    case 18:
                        field.cells[y, x] = new Struct.CellInfo("苔", 0, 2, -5, 0, 0, 0, 0);
                        break;
                    case 19:
                        field.cells[y, x] = new Struct.CellInfo("枯れ木", 0, 2, 10, 0, 0, 0, 0);
                        break;
                    case 20:
                        field.cells[y, x] = new Struct.CellInfo("石像", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 21:
                        field.cells[y, x] = new Struct.CellInfo("溶岩", 1, 1, 5, 1, 0, 0, 0);
                        break;
                    case 22:
                        field.cells[y, x] = new Struct.CellInfo("橋", 0, 1, 0, 0, 0, 0, 0);
                        break;
                    case 23:
                        field.cells[y, x] = new Struct.CellInfo("出入口", 0, 1, 0, 0, 0, 0, 0);
                        break;
                }
            }
        }
    }
}
