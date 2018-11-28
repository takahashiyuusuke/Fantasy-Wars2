using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int mapId = 0;  //マップID
    public Map mapManager;

    // マップ上のユニット配置リスト
    static UnitInfo[,] mapUnitData;

    private void Awake() {
        // マップデータ読み込み
        mapManager.LoadData(mapId);

        // ユニットの配置リストの初期化
        mapUnitData = new UnitInfo[Map.GetFieldData().height, Map.GetFieldData().width];
    }

    //
    public static UnitInfo GetMapUnit(Vector3 pos) {
        return mapUnitData[-(int)pos.y, (int)pos.x];
    }

    //配置リスト上の特定の座標ユニット情報を返す
    public static UnitInfo[,] GetMapUnitData() {
        return mapUnitData;
    }
    //配置リストにユニット情報を登録する
    public static void AddMapUnitData(Vector3 pos, UnitInfo unitInfo) {
        mapUnitData[-(int)pos.y, (int)pos.x] = unitInfo;
    }
    // 配置リスト上でユニット情報を移動する
    public static void MoveMapUnitData(Vector3 oldPos, Vector3 newPos) {
        mapUnitData[-(int)newPos.y, (int)newPos.x] = mapUnitData[-(int)oldPos.y, (int)oldPos.x];
        mapUnitData[-(int)oldPos.y, (int)newPos.x] = null;
    }
    //
}
