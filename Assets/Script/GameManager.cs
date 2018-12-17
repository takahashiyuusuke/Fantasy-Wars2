using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Novel;

namespace Main { 

public class GameManager : MonoBehaviour {

    public int mapId = 0;  //マップID
    public MapManager mapManager;

    // マップ上のユニット配置リスト
    static UnitInfo[,] mapUnitData;

    private void Awake() {
        // マップデータ読み込み
        mapManager.LoadData(mapId);
        
        // ユニットの配置リストの初期化
        mapUnitData = new UnitInfo[MapManager.GetFieldData().height, MapManager.GetFieldData().width];
    }
		void start() {
			switch (mapId) {
			case 1:
				NovelSingleton.StatusManager.callJoker ("tall/stage1_start", "");
				break;
			case 2:
				NovelSingleton.StatusManager.callJoker ("tall/stage2_start", "");
				break;
			case 3:
				NovelSingleton.StatusManager.callJoker ("tall/stage3_start", "");
				break;
			case 4:
				NovelSingleton.StatusManager.callJoker ("tall/stage4_start", "");
				break;
			case 5:
				NovelSingleton.StatusManager.callJoker ("tall/stage5_start", "");
				break;
			case 6:
				NovelSingleton.StatusManager.callJoker ("tall/stage6_start", "");
				break;
			case 7:
				NovelSingleton.StatusManager.callJoker ("tall/stage7_start", "");
				break;
			case 8:
				NovelSingleton.StatusManager.callJoker ("tall/stage8_start", "");
				break;
			case 9:
				NovelSingleton.StatusManager.callJoker ("tall/stage9_start", "");
				break;
			case 10:
				NovelSingleton.StatusManager.callJoker ("tall/stage10_start", "");
				break;
			}
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
        mapUnitData[-(int)oldPos.y, (int)oldPos.x] = null;
        Debug.Log("位置変更終了");
    }
}
}