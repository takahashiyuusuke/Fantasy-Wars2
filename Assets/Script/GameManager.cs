using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Main { 
    public class GameManager : MonoBehaviour {

        public int mapId = 0;  //マップID
        public MapManager mapManager;

        // マップに配置しているUnitオブジェクト
        static GameObject[,] mapUnitObj;

        private void Awake() {
            // マップデータ読み込み
            mapManager.LoadData(mapId);
        
            // ユニットの配置リストの初期化
            mapUnitObj = new GameObject[MapManager.GetFieldData().height, MapManager.GetFieldData().width];
        }
    

        // 配置リスト上の特定の座標のユニット情報を返す
        public static UnitInfo GetMapUnitInfo(Vector3 pos) {
            return mapUnitObj[-(int)pos.y, (int)pos.x] != null ? mapUnitObj[-(int)pos.y, (int)pos.x].GetComponent<UnitInfo>() : null;
        }
        
        // 配置リスト上の特定の座標のユニットオブジェクトを返す
        public static GameObject GetMapUnit(Vector3 pos) {
            return mapUnitObj[-(int)pos.y, (int)pos.x];
        }

        // ユニットの配置リストを返す
        public static GameObject[,] GetMapUnitData() {
            return mapUnitObj;
        }
        // 配置リストにユニット情報を登録する
        public static void AddMapUnitData(Vector3 pos, GameObject unitInfo) {
            mapUnitObj[-(int)pos.y, (int)pos.x] = unitInfo;
            
        }
        // 配置リスト上でユニット情報を移動する
        public static void MoveMapUnitData(Vector3 oldPos, Vector3 newPos) {
            mapUnitObj[-(int)newPos.y, (int)newPos.x] = mapUnitObj[-(int)oldPos.y, (int)oldPos.x];
            mapUnitObj[-(int)oldPos.y, (int)oldPos.x] = null;
            Debug.Log("位置変更終了");
        }

        // 配置リスト上のユニット情報を削除する
        public static void RemoveMapUnitData(Vector3 pos) {
            mapUnitObj[-(int)pos.y, (int)pos.x] = null;
        }
    }
}