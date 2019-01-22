using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Novel;

namespace Main {
    public class GameManager : MonoBehaviour {

        public int mapId = 0;  //マップID
        static MapManager mapManager;
        static UnitManager unitManager;
        static RouteManager routeManager;

        // マップに配置しているUnitオブジェクト
        //static GameObject[,] mapUnitObj;

        private void Awake() {
            // 各マネージャーの初期化
            mapManager = new MapManager(mapId);
            unitManager = new UnitManager(mapManager.field);
            routeManager = new RouteManager(mapManager.field);
        }
        void start() {

            // シナリオを読み込む
            switch (mapId)
            {
                case 1:
                    //NovelSingleton.StatusManager.callJoker("tall/stage1_start", "");
                    break;
                case 2:
                    //NovelSingleton.StatusManager.callJoker("tall/stage2_start", "");
                    break;
                case 3:
                    //NovelSingleton.StatusManager.callJoker("tall/stage3_start", "");
                    break;
                case 4:
                    //NovelSingleton.StatusManager.callJoker("tall/stage4_start", "");
                    break;
                case 5:
                    //NovelSingleton.StatusManager.callJoker("tall/stage5_start", "");
                    break;
                case 6:
                    //NovelSingleton.StatusManager.callJoker("tall/stage6_start", "");
                    break;
                case 7:
                    //NovelSingleton.StatusManager.callJoker("tall/stage7_start", "");
                    break;
                case 8:
                    //NovelSingleton.StatusManager.callJoker("tall/stage8_start", "");
                    break;
                case 9:
                    //NovelSingleton.StatusManager.callJoker("tall/stage9_start", "");
                    break;
                case 10:
                    //NovelSingleton.StatusManager.callJoker("tall/stage10_start", "");
                    break;
            }
        }


        //// 配置リスト上の特定の座標のユニット情報を返す
        //public static UnitInfo GetMapUnitInfo(Vector3 pos) {
        //    return mapUnitObj[-(int)pos.y, (int)pos.x] != null ? mapUnitObj[-(int)pos.y, (int)pos.x].GetComponent<UnitInfo>() : null;
        //}

        //// 配置リスト上の特定の座標のユニットオブジェクトを返す
        //public static GameObject GetMapUnit(Vector3 pos) {
        //    return mapUnitObj[-(int)pos.y, (int)pos.x];
        //}

        //// ユニットの配置リストを返す
        //public static GameObject[,] GetMapUnitData() {
        //    return mapUnitObj;
        //}
        //// 配置リストにユニット情報を登録する
        //public static void AddMapUnitData(Vector3 pos, GameObject unitInfo) {
        //    mapUnitObj[-(int)pos.y, (int)pos.x] = unitInfo;

        //}
        //// 配置リスト上でユニット情報を移動する
        //public static void MoveMapUnitData(Vector3 oldPos, Vector3 newPos) {
        //    mapUnitObj[-(int)newPos.y, (int)newPos.x] = mapUnitObj[-(int)oldPos.y, (int)oldPos.x];
        //    mapUnitObj[-(int)oldPos.y, (int)oldPos.x] = null;
        //    Debug.Log("位置変更終了");
        //}

        //// 配置リスト上のユニット情報を削除する
        //public static void RemoveMapUnitData(Vector3 pos) {
        //    mapUnitObj[-(int)pos.y, (int)pos.x] = null;
        //}

        /// <summary>
        /// 外部呼出し用
        /// </summary>
        /// <returns></returns>
        public static MapManager GetMap() {
            return mapManager;
        }

        /// <summary>
        /// 外部呼出し用
        /// </summary>
        /// <returns></returns>
        public static UnitManager GetUnit() { return unitManager; }

        /// <summary>
        /// 外部呼出し用
        /// </summary>
        /// <returns></returns>
        public static RouteManager GetRoute() { return routeManager; }
    }
}