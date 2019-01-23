﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// マップ移動でのルート算出クラス
/// </summary>
public class RouteManager {

    // ルートの算出に必要なフィールドデータ
    Struct.Field field;
    int fieldWidth, fieldHeight;

    public RouteManager(Struct.Field field) {
        this.field = field;
        this.fieldWidth = field.width;
        this.fieldHeight = field.height;
    }



    /// <summary>
    /// フォーカスユニットから目的地までの最短ルートをチェックし、
    /// ユニットの移動ルートリストに登録する
    /// </summary>
    /// <param name="cursorManager">Cursor manager.</param>
    /// <param name="endPos">End position.</param>
    public void CheckShortestRoute(ref PhaseManager phaseManager, Vector3 endPos) {
        // ユニットの移動ルートを初期化
        phaseManager.moveRoot = new List<Vector3>();

        // 開始地点から終了地点までたどり着けるか
        bool isEnd = false;
        Vector3 pos = phaseManager.focusUnitObj.transform.position;
        Struct.NodeMove[,] nodeList = new Struct.NodeMove[fieldHeight, fieldWidth];
        nodeList[-(int)pos.y, (int)pos.x].aREA = Enums.AREA.UNIT;

        // スタート地点からエンドまで再帰的に移動コストをチェックする
        CheckRootAreaRecursive(ref nodeList, ref phaseManager.activeAreaManager.activeAreaList, pos, ref endPos, 0, ref isEnd);

        // ゴールできる場合は最短ルートをチェックする
        if (isEnd) phaseManager.moveRoot = CheckShootRootRecursive(ref phaseManager, ref nodeList, endPos);
    }

    /// <summary>
    /// CheckShortestRouteクラスの再帰的呼び出し処理（実際のチェック処理）
    /// </summary>
    /// <param name="nodeList">Node list.</param>
    /// <param name="activeAreaList">Active area list.</param>
    /// <param name="checkPos">Check position.</param>
    /// <param name="endPos">End position.</param>
    /// <param name="previousCost">Previous cost.</param>
    /// <param name="isEnd">If set to <c>true</c> is end.</param>
    private void CheckRootAreaRecursive(ref Struct.NodeMove[,] nodeList, ref Struct.NodeMove[,] activeAreaList, Vector3 checkPos, ref Vector3 endPos, int previousCost, ref bool isEnd) {
        // 配列の外（マップ外）なら何もしない
        if (-(int)checkPos.y < 0 ||
            fieldHeight <= -checkPos.y ||
            checkPos.x < 0 ||
            fieldWidth <= checkPos.x)
            return;

        // アクティブエリアでなければ何もしない
        if (activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA != Enums.AREA.MOVE &&
            activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA != Enums.AREA.UNIT)
            return;

        // 省コストで上書きできない場合は終了
        if (nodeList[-(int)checkPos.y, (int)checkPos.x].cost != 0 &&
            nodeList[-(int)checkPos.y, (int)checkPos.x].cost <=
            previousCost + field.cells[-(int)checkPos.y, (int)checkPos.x].moveCost)
            return;

        // 移動前のコストと今回のコストを合計して設定する（開始地点を除く）
        if (nodeList[-(int)checkPos.y, (int)checkPos.x].aREA != Enums.AREA.UNIT)
            nodeList[-(int)checkPos.y, (int)checkPos.x].cost = previousCost + field.cells[-(int)checkPos.y, (int)checkPos.x].moveCost;

        // ゴールまで辿り着ける事を確認した
        if (checkPos == endPos) isEnd = true;

        // 次に検証する座標を指定（上下左右）
        CheckRootAreaRecursive(ref nodeList, ref activeAreaList, checkPos + Vector3.up, ref endPos, nodeList[-(int)checkPos.y, (int)checkPos.x].cost, ref isEnd);
        CheckRootAreaRecursive(ref nodeList, ref activeAreaList, checkPos + Vector3.down, ref endPos, nodeList[-(int)checkPos.y, (int)checkPos.x].cost, ref isEnd);
        CheckRootAreaRecursive(ref nodeList, ref activeAreaList, checkPos + Vector3.left, ref endPos, nodeList[-(int)checkPos.y, (int)checkPos.x].cost, ref isEnd);
        CheckRootAreaRecursive(ref nodeList, ref activeAreaList, checkPos + Vector3.right, ref endPos, nodeList[-(int)checkPos.y, (int)checkPos.x].cost, ref isEnd);
    }

    /// <summary>
    /// CheckShortestRouteクラスの再帰的呼び出し処理（最短ルートの算出）
    /// </summary>
    /// <returns>The end root.</returns>
    /// <param name="cursorManager">Cursor manager.</param>
    /// <param name="nodeList">Node list.</param>
    /// <param name="checkPos">Check position.</param>
    private List<Vector3> CheckShootRootRecursive(ref PhaseManager phaseManager, ref Struct.NodeMove[,] nodeList, Vector3 checkPos) {
        // 目的地からスタート位置（ユニット）まで辿り着いたら移動ルートを返す
        if (checkPos == phaseManager.focusUnitObj.transform.position) return phaseManager.moveRoot;

        // 上下左右のコストをチェクし昇順に並び替える
        List<Struct.NodeRoot> list = new List<Struct.NodeRoot>();
        int valUp = CheckNodeCost(ref nodeList, ref phaseManager.activeAreaManager.activeAreaList, checkPos + Vector3.up);
        if (-1 < valUp) list.Insert(list.Count, new Struct.NodeRoot(Enums.MOVE.UP, valUp));
        int valDown = CheckNodeCost(ref nodeList, ref phaseManager.activeAreaManager.activeAreaList, checkPos + Vector3.down);
        if (-1 < valDown) list.Insert(list.Count, new Struct.NodeRoot(Enums.MOVE.DOWN, valDown));
        int valLeft = CheckNodeCost(ref nodeList, ref phaseManager.activeAreaManager.activeAreaList, checkPos + Vector3.left);
        if (-1 < valLeft) list.Insert(list.Count, new Struct.NodeRoot(Enums.MOVE.LEFT, valLeft));
        int valRight = CheckNodeCost(ref nodeList, ref phaseManager.activeAreaManager.activeAreaList, checkPos + Vector3.right);
        if (-1 < valRight) list.Insert(list.Count, new Struct.NodeRoot(Enums.MOVE.RIGHT, valRight));
        list.Sort((a, b) => a.cost.CompareTo(b.cost));

        // もっともコストの低いマスを次のチェック対象とする
        switch (list[0].move)
        {
            case Enums.MOVE.UP:
                phaseManager.moveRoot.Insert(0, Vector3.down);
                CheckShootRootRecursive(ref phaseManager, ref nodeList, checkPos + Vector3.up);
                break;

            case Enums.MOVE.DOWN:
                phaseManager.moveRoot.Insert(0, Vector3.up);
                CheckShootRootRecursive(ref phaseManager, ref nodeList, checkPos + Vector3.down);
                break;

            case Enums.MOVE.LEFT:
                phaseManager.moveRoot.Insert(0, Vector3.right);
                CheckShootRootRecursive(ref phaseManager, ref nodeList, checkPos + Vector3.left);
                break;

            case Enums.MOVE.RIGHT:
                phaseManager.moveRoot.Insert(0, Vector3.left);
                CheckShootRootRecursive(ref phaseManager, ref nodeList, checkPos + Vector3.right);
                break;
        }
        return phaseManager.moveRoot;
    }

    /// <summary>
    /// CheckShootRootRecursiveで使用する上下左右のコストチェック関数
    /// </summary>
    /// <returns>The node cost.</returns>
    /// <param name="nodeList">Node list.</param>
    /// <param name="activeAreaList">Active area list.</param>
    /// <param name="checkPos">Check position.</param>
    private int CheckNodeCost(ref Struct.NodeMove[,] nodeList, ref Struct.NodeMove[,] activeAreaList, Vector3 checkPos) {
        // 配列の外（マップ外）なら何もしない
        if (-(int)checkPos.y < 0 ||
            fieldHeight <= -checkPos.y ||
            checkPos.x < 0 ||
            fieldWidth <= checkPos.x)
            return -1;

        // アクティブエリアでなければ-1を返す
        if (activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA != Enums.AREA.MOVE &&
            activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA != Enums.AREA.UNIT)
            return -1;

        // 移動コストを返す
        return nodeList[-(int)checkPos.y, (int)checkPos.x].cost; ;
    }

    /// <summary>
    /// フォーカスユニットの移動エリアの算出
    /// </summary>
    /// <param name="cursorManager">Cursor manager.</param>
    public void CheckMoveArea(ref PhaseManager phaseManager) {
        // スタート地点からエンドまで再帰的に移動コストをチェックする
        Vector3 pos = phaseManager.focusUnitObj.transform.position;
        phaseManager.activeAreaManager.activeAreaList = new Struct.NodeMove[fieldHeight, fieldWidth];
        phaseManager.activeAreaManager.activeAreaList[-(int)pos.y, (int)pos.x].aREA = Enums.AREA.UNIT;

        CheckMoveAreaRecursive(ref phaseManager, pos, 0);
    }

    /// <summary>
    /// CheckMoveAreaクラスの再帰的算出クラス
    /// </summary>
    /// <param name="cursorManager">Cursor manager.</param>
    /// <param name="checkPos">Check position.</param>
    /// <param name="previousCost">Previous cost.</param>
    private void CheckMoveAreaRecursive(ref PhaseManager phaseManager, Vector3 checkPos, int previousCost) {
        // 配列の外（マップ外）なら何もしない
        if (-(int)checkPos.y < 0 ||
            fieldHeight <= -checkPos.y ||
            checkPos.x < 0 ||
            fieldWidth <= checkPos.x)
            return;

        // キャラが移動できないマスなら何もしない
        if (!isMoveing(field.cells[-(int)checkPos.y, (int)checkPos.x].category, phaseManager.focusUnitObj.GetComponent<UnitInfo>().moveType))
            return;

        // 移動先にユニットがいた場合のすり抜けチェック
        if (Main.GameManager.GetUnit().GetMapUnitInfo(checkPos))
        {
            switch (Main.GameManager.GetUnit().GetMapUnitInfo(checkPos).aRMY)
            {
                case Enums.ARMY.ALLY:
                    if (phaseManager.focusUnitObj.GetComponent<UnitInfo>().aRMY == Enums.ARMY.ENEMY)
                        return;
                    break;
                case Enums.ARMY.ENEMY:
                    if (phaseManager.focusUnitObj.GetComponent<UnitInfo>().aRMY == Enums.ARMY.ALLY ||
                        phaseManager.focusUnitObj.GetComponent<UnitInfo>().aRMY == Enums.ARMY.NEUTRAL)
                        return;
                    break;
                case Enums.ARMY.NEUTRAL:
                    if (phaseManager.focusUnitObj.GetComponent<UnitInfo>().aRMY == Enums.ARMY.ENEMY)
                        return;
                    break;
            }
        }

        // 省コストで上書きできない場合は終了
        if (phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost != 0 &&
            phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost <=
            previousCost + field.cells[-(int)checkPos.y, (int)checkPos.x].moveCost)
            return;

        // 移動前のコストと今回のコストを合計して設定する（開始地点を除く）
        if (phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA != Enums.AREA.UNIT)
        {
            phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost = previousCost + field.cells[-(int)checkPos.y, (int)checkPos.x].moveCost;
            phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA = Enums.AREA.MOVE;
        }

        // 移動コストを超えた場合は終了
        if (phaseManager.focusUnitObj.GetComponent<UnitInfo>().movementRange <= phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost) return;

        // 次に検証する座標を指定（上下左右）
        CheckMoveAreaRecursive(ref phaseManager, checkPos + Vector3.up, phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost);
        CheckMoveAreaRecursive(ref phaseManager, checkPos + Vector3.down, phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost);
        CheckMoveAreaRecursive(ref phaseManager, checkPos + Vector3.left, phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost);
        CheckMoveAreaRecursive(ref phaseManager, checkPos + Vector3.right, phaseManager.activeAreaManager.activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost);
    }

    /// <summary>
    /// フォーカスユニットの攻撃エリアの算出
    /// </summary>
    /// <param name="list">List.</param>
    /// <param name="startPos">Start position.</param>
    /// <param name="focusUnit">Focus unit.</param>
    public void CheckAttackArea(ref Struct.NodeMove[,] list, Vector3 startPos, int attackRange) {
        // 開始地点を基準に上下左右を検証
        CheckAttackAreaRecursive(ref list, startPos, Vector3.up, attackRange);
        CheckAttackAreaRecursive(ref list, startPos, Vector3.down, attackRange);
        CheckAttackAreaRecursive(ref list, startPos, Vector3.left, attackRange);
        CheckAttackAreaRecursive(ref list, startPos, Vector3.right, attackRange);
    }

    /// <summary>
    /// CheckAttackAreaクラスの再帰的算出クラス
    /// </summary>
    /// <param name="activeAreaList">Active area list.</param>
    /// <param name="checkPos">Check position.</param>
    /// <param name="nextPos">Next position.</param>
    /// <param name="previousCost">Previous cost.</param>
    private void CheckAttackAreaRecursive(ref Struct.NodeMove[,] activeAreaList, Vector3 checkPos, Vector3 nextPos, int previousCost) {
        // 攻撃範囲を超えた場合は終了
        if (previousCost <= 0) return;

        // コストの算出
        checkPos += nextPos;

        // 配列の外（マップ外）なら何もしない
        if (-(int)checkPos.y < 0 ||
            fieldHeight <= -checkPos.y ||
            checkPos.x < 0 ||
            fieldWidth <= checkPos.x)
            return;

        if (activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA == Enums.AREA.NONE)
        {
            // 移動エリアでなければ攻撃範囲として登録
            previousCost--;
            activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost = previousCost;
            activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA = Enums.AREA.ATTACK;
        }
        else if (activeAreaList[-(int)checkPos.y, (int)checkPos.x].aREA == Enums.AREA.ATTACK &&
                 activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost < previousCost - 1)
        {
            // 既に攻撃範囲として登録されていても、少ないコストで上書き出来るならば上書きする
            previousCost--;
            activeAreaList[-(int)checkPos.y, (int)checkPos.x].cost = previousCost;
        }
        else return; // 条件に合致しない場合は、そこで探索終了する


        // 次に検証する座標を指定（上下左右）
        if (nextPos != Vector3.down) CheckAttackAreaRecursive(ref activeAreaList, checkPos, Vector3.up, previousCost);
        if (nextPos != Vector3.up) CheckAttackAreaRecursive(ref activeAreaList, checkPos, Vector3.down, previousCost);
        if (nextPos != Vector3.right) CheckAttackAreaRecursive(ref activeAreaList, checkPos, Vector3.left, previousCost);
        if (nextPos != Vector3.left) CheckAttackAreaRecursive(ref activeAreaList, checkPos, Vector3.right, previousCost);
    }

    /// <summary>
    /// ユニットタイプ毎の移動可能セルのチェック
    /// </summary>
    /// <returns><c>true</c>, if moveing was ised, <c>false</c> otherwise.</returns>
    /// <param name="cellCategory">Cell category.</param>
    /// <param name="moveType">Move type.</param>
    public static bool isMoveing(int cellCategory, Enums.MOVE_TYPE moveType) {
        // キャラ毎の移動可能かどうかのチェック
        switch (moveType)
        {
            case Enums.MOVE_TYPE.WALKING:
                if (cellCategory == 1) return false;
                break;
            case Enums.MOVE_TYPE.ATHLETE: break;
            case Enums.MOVE_TYPE.HORSE: break;
            case Enums.MOVE_TYPE.FLYING: break;
        }
        return true;
    }
}