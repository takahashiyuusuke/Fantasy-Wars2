using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int mapId = 0;
    public Map mapManager;

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
    //
    public static UnitInfo[,] GetMapData() {
        return mapUnitData;
    }
    //
    public static void AddMapUnitData(Vector3 pos, UnitInfo unitInfo) {
        mapUnitData[-(int)pos.y, (int)pos.x] = unitInfo;
    }
    //
    public static void MoveMapUnitData(Vector3 oldPos, Vector3 newPos) {
        mapUnitData[-(int)newPos.y, (int)newPos.x] = mapUnitData[-(int)oldPos.y, (int)oldPos.x];
        mapUnitData[-(int)oldPos.y, (int)newPos.x] = null;
    }
    //
    public static void RemoveMap() {

    }
}
